using RLTracker.Models.Session;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Tracker;

namespace Common.Models
{
    public static class ViewModelMapper
    {
        public static void TrackedUser(TrackedUser user, TrackedUserViewModel vm)
        {
            var defaultUri = @"https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-0.png";

            try
            {
                //This could probably be done with automapper, but the config would be just as ugly as this
                vm.Name = user.Data.PlatformInfo.PlatformUserHandle;
                vm.UserId = user.UserId;
                vm.Platform = user.PlatForm;
                vm.Avatar = user.Data.PlatformInfo.AvatarUrl;
                vm.PlayerUri = new Uri($"https://rocketleague.tracker.network/rocket-league/profile/{Common.Search.RlTracker.GetPlatformString(user.PlatForm)}/{user.UserId}");
                vm.SortOrder = user.SortOrder;

                var causal = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 0);
                if (vm.CasualModel == null)
                    vm.CasualModel = new RankViewModel();
                vm.CasualModel.ImageUrl = causal?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.CasualModel.Mmr = causal?.Stats.Rating.Value;
                vm.CasualModel.MatchesPlayed = causal?.Stats.MatchesPlayed.Value;


                var threes = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 13);
                if (vm.ThreesModel == null)
                    vm.ThreesModel = new RankViewModel();
                vm.ThreesModel.ImageUrl = threes?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.ThreesModel.Title = threes?.Stats.Tier.Metadata.Name;
                vm.ThreesModel.Mmr = threes?.Stats.Rating.Value;
                vm.ThreesModel.MatchesPlayed = threes?.Stats.MatchesPlayed.Value;
                vm.ThreesModel.DivDown = threes?.Stats.Division.Metadata.DeltaDown;
                vm.ThreesModel.DivUp = threes?.Stats.Division.Metadata.DeltaUp;
                vm.ThreesModel.Division = threes?.Stats.Division.Metadata.Name;



                var twos = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 11);
                if (vm.TwosModel == null)
                    vm.TwosModel = new RankViewModel();
                vm.TwosModel.ImageUrl = twos?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.TwosModel.Title = twos?.Stats.Tier.Metadata.Name;
                vm.TwosModel.Mmr = twos?.Stats.Rating.Value;
                vm.TwosModel.MatchesPlayed = twos?.Stats.MatchesPlayed.Value;
                vm.TwosModel.DivDown = twos?.Stats.Division.Metadata.DeltaDown;
                vm.TwosModel.DivUp = twos?.Stats.Division.Metadata.DeltaUp;
                vm.TwosModel.Division = twos?.Stats.Division.Metadata.Name;

                var ones = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 10);
                if (vm.OnesModel == null)
                    vm.OnesModel = new RankViewModel();
                vm.OnesModel.ImageUrl = ones?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.OnesModel.Title = ones?.Stats.Tier.Metadata.Name;
                vm.OnesModel.Mmr = ones?.Stats.Rating.Value;
                vm.OnesModel.MatchesPlayed = ones?.Stats.MatchesPlayed.Value;
                vm.OnesModel.DivDown = ones?.Stats.Division.Metadata.DeltaDown;
                vm.OnesModel.DivUp = ones?.Stats.Division.Metadata.DeltaUp;
                vm.OnesModel.Division = ones?.Stats.Division.Metadata.Name;


                var tournament = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 34);
                if (vm.TournamentModel == null)
                    vm.TournamentModel = new RankViewModel();
                vm.TournamentModel.ImageUrl = tournament?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.TournamentModel.Title = tournament?.Stats.Tier.Metadata.Name;
                vm.TournamentModel.Mmr = tournament?.Stats.Rating.Value;

                var rumble = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 28);
                if (vm.RumbleModel == null)
                    vm.RumbleModel = new RankViewModel();
                vm.RumbleModel.ImageUrl = rumble?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.RumbleModel.Title = rumble?.Stats.Tier.Metadata.Name;
                vm.RumbleModel.Mmr = rumble?.Stats.Rating.Value;
                vm.RumbleModel.MatchesPlayed = rumble?.Stats.MatchesPlayed.Value;
                vm.RumbleModel.DivDown = rumble?.Stats.Division.Metadata.DeltaDown;
                vm.RumbleModel.DivUp = rumble?.Stats.Division.Metadata.DeltaUp;
                vm.RumbleModel.Division = rumble?.Stats.Division.Metadata.Name;

                var dropshot = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 29);
                if (vm.DropshotModel == null)
                    vm.DropshotModel = new RankViewModel();
                vm.DropshotModel.ImageUrl = dropshot?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.DropshotModel.Title = dropshot?.Stats.Tier.Metadata.Name;
                vm.DropshotModel.Mmr = dropshot?.Stats.Rating.Value;
                vm.DropshotModel.MatchesPlayed = dropshot?.Stats.MatchesPlayed.Value;
                vm.DropshotModel.DivDown = dropshot?.Stats.Division.Metadata.DeltaDown;
                vm.DropshotModel.DivUp = dropshot?.Stats.Division.Metadata.DeltaUp;
                vm.DropshotModel.Division = dropshot?.Stats.Division.Metadata.Name;

                var hoops = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 27);
                if (vm.HoopsModel == null)
                    vm.HoopsModel = new RankViewModel();
                vm.HoopsModel.ImageUrl = hoops?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.HoopsModel.Title = hoops?.Stats.Tier.Metadata.Name;
                vm.HoopsModel.Mmr = hoops?.Stats.Rating.Value;
                vm.HoopsModel.MatchesPlayed = hoops?.Stats.MatchesPlayed.Value;
                vm.HoopsModel.DivDown = hoops?.Stats.Division.Metadata.DeltaDown;
                vm.HoopsModel.DivUp = hoops?.Stats.Division.Metadata.DeltaUp;
                vm.HoopsModel.Division = hoops?.Stats.Division.Metadata.Name;

                var snowday = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 30);
                if (vm.SnowdayModel == null)
                    vm.SnowdayModel = new RankViewModel();
                vm.SnowdayModel.ImageUrl = snowday?.Stats.Tier.Metadata.IconUrl?.ToString();
                vm.SnowdayModel.Title = snowday?.Stats.Tier.Metadata.Name;
                vm.SnowdayModel.Mmr = snowday?.Stats.Rating.Value;
                vm.SnowdayModel.MatchesPlayed = snowday?.Stats.MatchesPlayed.Value;
                vm.SnowdayModel.DivDown = snowday?.Stats.Division.Metadata.DeltaDown;
                vm.SnowdayModel.DivUp = snowday?.Stats.Division.Metadata.DeltaUp;
                vm.SnowdayModel.Division = snowday?.Stats.Division.Metadata.Name;
            
            }
            catch (Exception)
            {

            }
        }

        public static void SettingsViewModel(AppSettings settings, SettingsViewModel model)
        {
            model.AutoUpdate = settings.AutoUpdate;
            model.MinimizeToTray = settings.MinimizeToTray.Value;
            model.RefreshMinutes = settings.RefreshMins.Value;
            model.SaveLocation = settings.SaveFolderLocation;
            model.SelectedColor = settings.Color;
            model.UseDarkMode = settings.UseDarkMode.Value;
        }

        public static void SessionViewModel(Session session, SessionsViewModel model, string playername)
        {
            string StatusParser(string input, long? delta)
            {
                if (delta < 0)
                    return "Defeat";

                if (input.Contains("wins"))
                {
                    return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.Trim().ToLower());
                }
                if(input == "victory")
                {
                    return "Win";
                }

                return "Defeat";
            }

            foreach (var item in session.Data.Items)
            {
                var m = new SessionViewModel();
                m.Time = item.Metadata.EndDate.DisplayValue.Value.LocalDateTime;
                m.Player = playername;

                foreach (var match in item.Matches)
                {
                    var matchViewModel = new MatchViewModel()
                    {
                        Mode = match.Metadata.Playlist,
                        Won = match.Stats.Rating.Metadata.RatingDelta > 0,
                        Status = StatusParser(match.Metadata.Result, match.Stats.Rating.Metadata.RatingDelta),
                        Mmr = match.Stats.Rating.Value.Value,
                        Delta = match.Stats.Rating.Metadata.RatingDelta.HasValue ? match.Stats.Rating.Metadata.RatingDelta.Value : 0,
                        IconUrl = match.Stats.Rating.Metadata.IconUrl != null ? match.Stats.Rating.Metadata.IconUrl.ToString() : "",
                        Division = match.Stats.Rating.Metadata.Division,
                        Tier = match.Stats.Rating.Metadata.Tier
                    };
                    matchViewModel.Pic = ImageManager.Instance().GetImageFromUri(matchViewModel.IconUrl);
                    m.Matches.Add(matchViewModel);
                }

                model.Sessions.Add(m);
            }
        }
    }
}
