using System;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Common.Models
{
    //Todo create specific objects for the various related game modes
    public class TrackedUserViewModel : INotifyPropertyChanged, IComparable
    {
        private string name;
        private long userId;
        private Platform platform;
        private Uri avatar;
        private Uri playerUri;

        public RankViewModel ThreesModel { get; set; }
        public RankViewModel TwosModel { get; set; }
        public RankViewModel OnesModel { get; set; }
        public RankViewModel CasualModel { get; set; }
        public RankViewModel TournamentModel { get; set; }

        public RankViewModel RumbleModel { get; set; }
        public RankViewModel DropshotModel { get; set; }
        public RankViewModel HoopsModel { get; set; }
        public RankViewModel SnowdayModel { get; set; }

        private SessionsViewModel _session;
        public SessionsViewModel Sessions 
        {
            get
            {
                if (_session == null)
                    _session = new SessionsViewModel();

                return _session;
            }
            set
            {
                _session = value;
                NotifyPropertyChanged(nameof(Sessions));
            }
        }

        public string Name { get => name; set { if (name != value) { name = value; NotifyPropertyChanged(); } } }
        public long UserId { get => userId; set { if (userId != value) { userId = value; NotifyPropertyChanged(); } } }
        public Platform Platform { get => platform; set { if (platform != value) { platform = value; NotifyPropertyChanged(); } } }
        public Uri Avatar { get => avatar; set { if (avatar != value) { avatar = value; NotifyPropertyChanged(); } } }

        public Uri PlayerUri { get => playerUri; set { if (playerUri != value) { playerUri = value; } } }

        private int? sortOrder;
        public int? SortOrder { get { if (sortOrder.HasValue == false) { return 99; } return sortOrder; } set { if (sortOrder != value) { sortOrder = value; NotifyPropertyChanged(nameof(SortOrder)); } } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object obj)
        {
            TrackedUserViewModel a = this;
            TrackedUserViewModel b = (TrackedUserViewModel)obj;

            if (a.SortOrder == b.SortOrder)
                return this.UserId.CompareTo(b.UserId);

            return a.SortOrder.Value.CompareTo(b.SortOrder.Value);
        }
    }
}
