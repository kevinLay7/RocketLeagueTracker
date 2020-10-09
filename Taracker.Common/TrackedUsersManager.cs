using Common;
using Common.Models;
using Common.Search;
using Newtonsoft.Json;
using RLTracker.Models.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Tracker
{

    /// <summary>
    /// Object containing tracked users and is responsible for maintainging them
    /// </summary>
    public class TrackedUsersManager : INotifyPropertyChanged
    {
        private readonly string trackedUsersFile = Constants.SaveLocation + "tracked.json";
        private RlTracker _searcher;
        private CancellationTokenSource _tokenSource;
        private SynchronizationContext _context;
        private AppSettings _settings;


        public event EventHandler<Session> SessionUserUpdated;

        private TrackedUser _sessionUser;
        private Session session;
        public Session Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
                SessionUserUpdated?.Invoke(this, session);
            }
        }


        private ObservableCollection<TrackedUser> _users;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TrackedUser> Users
        {
            get
            {
                _users.Sort();
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        private DateTime _nextUpdate;
        public DateTime NextUpdateTime { get => _nextUpdate;
            set
            {
                if(_nextUpdate != value)
                {
                    _nextUpdate = value;
                    NotifyPropertyChanged(nameof(NextUpdateTime));
                }
            }
        }

        #region Constructors

        /// <summary>
        /// Manager for tracked users
        /// </summary>
        /// <param name="searcher">Instance used to Update tracked users</param>
        public TrackedUsersManager(RlTracker searcher, AppSettings settings) : base()
        {
            _settings = settings;
            _users = new ObservableCollection<TrackedUser>();
            _searcher = searcher;
            _tokenSource = new CancellationTokenSource();
            _context = SynchronizationContext.Current;

            NextUpdateTime = DateTime.Now;
        }

        #endregion

        #region Public

        /// <summary>
        /// Start background refreshing
        /// </summary>
        public async void StartMonitor()
        {
            Load();
            var token = _tokenSource.Token;
            await BackgroundProcessing(token);
        }

        /// <summary>
        /// Stop background refreshing
        /// </summary>
        public void StopMonitor()
        {
            _tokenSource.Cancel();
        }

        /// <summary>
        /// Track a new user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="platform"></param>
        public async void Add(string userId, string platform)
        {
            var plat = RlTracker.GetPlatform(platform);

            var trackedUser = new TrackedUser()
            {
                PlatForm = plat,
                UserId = long.Parse(userId)
            };

            await RefreshUser(trackedUser);

            if (!_users.Any(x => x.UserId == trackedUser.UserId))
            {
                trackedUser.SortOrder = this._users.Count;
                _users.Add(trackedUser);
            }

            Save();
        }

        /// <summary>
        /// Remove a tracked user
        /// </summary>
        /// <param name="userId"></param>
        public void Remove(string userId)
        {
            var l = long.Parse(userId);
            if (!_users.Any(x => x.UserId == l))
                return;

            var user = _users.First(x => x.UserId == l);

            _users.Remove(user);

            var userSortIndex = user.SortOrder;

            if (userSortIndex != null)
            {
                foreach (var u in _users.Where(x => x.SortOrder > userSortIndex))
                {
                    u.SortOrder -= 1;
                }
            }

            Save();
        }

        /// <summary>
        /// For retrieving the latest info from TRN
        /// </summary>
        public void ForceRefresh()
        {
            RefreshTrackedUsers(true);
            Save();
            NextUpdateTime = DateTime.Now.AddMinutes(_settings.RefreshMins.Value);
        }

        /// <summary>
        /// Check if a user is already being tracked based on the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsTracked(string userId)
        {
            var l = long.Parse(userId);
            return _users.Any(x => x.UserId == l);
        }

        public void ShiftUp(long userId)
        {
            var reference = _users.First(x => x.UserId == userId);
            var oldIndex = reference.SortOrder;
            var newIndex = reference.SortOrder - 1;
            if (newIndex < 0 || newIndex >= _users.Count)
                return;

            var itemToSwitch = _users.First(x => x.SortOrder == newIndex);
            itemToSwitch.SortOrder = oldIndex;
            reference.SortOrder = newIndex;

            _ = Users;
            Save();
        }

        public void ShiftDown(long userId)
        {
            var reference = Users.First(x => x.UserId == userId);
            var oldIndex = reference.SortOrder;
            var newIndex = reference.SortOrder + 1;
            if (newIndex < 0 || newIndex >= _users.Count)
                return;

            var itemToSwitch = Users.First(x => x.SortOrder == newIndex);
            itemToSwitch.SortOrder = oldIndex;
            reference.SortOrder = newIndex;

            _ = Users;
            Save();
        }

        public async Task TrackUserSession(long userId)
        {
           _sessionUser = _users.FirstOrDefault(x => x.UserId == userId);
           Session = await _searcher.GetUserSession(userId, _sessionUser.PlatForm);
        }

        public void StopTrackingUserSession()
        {
            _sessionUser = null;

        }

        #endregion

        #region Persistence

        private void Load()
        {
            if (!System.IO.File.Exists(trackedUsersFile))
                return;
            var settingString = System.IO.File.ReadAllText(trackedUsersFile);

            if (string.IsNullOrEmpty(settingString))
                return;

            _users.Clear();

            var users = JsonConvert.DeserializeObject<List<TrackedUser>>(settingString);

            int i = 0;
            foreach (var user in users)
            {
                if (user.SortOrder == null)
                    user.SortOrder = i;

                _users.Add(user);
                i++;
            }
        }

        public void Save()
        {
            try
            {
                var stringInfo = JsonConvert.SerializeObject(_users);
                System.IO.File.WriteAllText(trackedUsersFile, stringInfo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        #endregion

        #region Private

        private Task BackgroundProcessing(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (DateTime.Now >= NextUpdateTime)
                    {
                        var changes = RefreshTrackedUsers();
                        if (changes)
                            Save();

                        if(_sessionUser != null)
                        {
                            var s = await _searcher.GetUserSession(_sessionUser.UserId, _sessionUser.PlatForm);
                            _context.Send(x => Session = s, null);
                        }

                        NextUpdateTime = DateTime.Now.AddMinutes(_settings.RefreshMins.Value);
                    }

                    await Task.Delay(1000);
                }
            }, token);
        }

        private bool RefreshTrackedUsers(bool force = false)
        {
            bool changes = false;
            var temp = _users.ToArray();

            Parallel.For(0, temp.Length, async i =>
            {
                var user = temp[i];
                try
                {
                    await RefreshUser(user);
                    _context.Send(x => _users[i] = user, null);
                    changes = true;
                }
                catch (Exception)
                {
                }
            });

            return changes;
        }

        private async Task RefreshUser(TrackedUser user)
        {
            user.Data = await _searcher.GetUser(user.UserId, user.PlatForm);
            user.LastUpdate = DateTime.Now;
            System.Diagnostics.Debug.WriteLine($"Refreshed user {user.Data.PlatformInfo.PlatformUserHandle}");
        }

        #endregion

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
