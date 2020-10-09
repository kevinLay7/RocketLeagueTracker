using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Common.Models
{
    public class RankViewModel : INotifyPropertyChanged
    {
        private string title;
        private double? mmr;
        private string division;
        private double? divUp;
        private double? divDown;
        private string imageUrl;
        private double? matchesPlayed;

        public string Title { get => title; set { if (title != value) { title = value; NotifyPropertyChanged(nameof(Title)); } } }
        public double? Mmr { get => mmr; set { if (mmr != value) { mmr = value; NotifyPropertyChanged(nameof(Mmr)); } } }
        public string Division { get => division; set  { if (division != value) { division = value; NotifyPropertyChanged(nameof(Division)); } } }
        public double? DivUp { get => divUp; set { if (divUp != value && value != null) { divUp = value; NotifyPropertyChanged(nameof(DivUp)); } } }
        public double? DivDown { get => divDown; set  { if (divDown != value && value != null) { divDown = value; NotifyPropertyChanged(nameof(DivDown)); } } }
        public string ImageUrl { get => imageUrl; set  { if (imageUrl != value) { imageUrl = value; NotifyPropertyChanged(nameof(ImageUrl)); } } }
        public double? MatchesPlayed { get => matchesPlayed; set { if (matchesPlayed != value) { matchesPlayed = value; NotifyPropertyChanged(nameof(MatchesPlayed)); } } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
