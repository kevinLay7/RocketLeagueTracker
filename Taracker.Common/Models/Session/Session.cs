using System;
using System.Collections.Generic;

namespace RLTracker.Models.Session
{

    public partial class Session
    {
        public Data Data { get; set; }
    }

    public partial class Data
    {
        public DateTimeOffset? ExpiryDate { get; set; }
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        public ItemMetadata Metadata { get; set; }
        public List<Match> Matches { get; set; }
        public Stats Stats { get; set; }
        public List<object> Playlists { get; set; }
    }

    public partial class Match
    {
        public Guid? Id { get; set; }
        public MatchMetadata Metadata { get; set; }
        public MatchStats Stats { get; set; }
    }

    public partial class MatchMetadata
    {
        public string Result { get; set; }
        public string Playlist { get; set; }
        public DateTimeOffset? DateCollected { get; set; }
    }

    public partial class MatchStats
    {
        public Stats Saves { get; set; }
        public Stats Assists { get; set; }
        public Stats Goals { get; set; }
        public Stats MatchesPlayed { get; set; }
        public Stats Mvps { get; set; }
        public Stats Shots { get; set; }
        public Stats Wins { get; set; }
        public Rating Rating { get; set; }
    }

    public partial class Stats
    {
        public object Rank { get; set; }
        public object Percentile { get; set; }
        public string DisplayName { get; set; }
        public object DisplayCategory { get; set; }
        public object Category { get; set; }
        public Stats Metadata { get; set; }
        public long? Value { get; set; }
        public long? DisplayValue { get; set; }
        public string DisplayType { get; set; }
    }

    public partial class Stats
    {
    }

    public partial class Rating
    {
        public object Rank { get; set; }
        public object Percentile { get; set; }
        public string DisplayName { get; set; }
        public object DisplayCategory { get; set; }
        public object Category { get; set; }
        public RatingMetadata Metadata { get; set; }
        public long? Value { get; set; }
        public string DisplayValue { get; set; }
        public string DisplayType { get; set; }
    }

    public partial class RatingMetadata
    {
        public Uri IconUrl { get; set; }
        public string Tier { get; set; }
        public string Division { get; set; }
        public long? RatingDelta { get; set; }
    }

    public partial class ItemMetadata
    {
        public Date StartDate { get; set; }
        public Date EndDate { get; set; }
    }

    public partial class Date
    {
        public DateTimeOffset? Value { get; set; }
        public DateTimeOffset? DisplayValue { get; set; }
    }


}
