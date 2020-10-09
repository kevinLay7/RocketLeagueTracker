using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Common.Models
{
    public class SessionsViewModel
    {
        public SessionsViewModel()
        {
            Sessions = new List<SessionViewModel>();
        }

        public List<SessionViewModel> Sessions { get; set; }
    }

    public class SessionViewModel
    {
        public SessionViewModel()
        {
            this.Matches = new List<MatchViewModel>();
        }

        public string Player { get; set; }

        public string TimeString
        {
            get
            {
                var diff = DateTime.Now - Time.Value.ToLocalTime();
                if (diff.Days == 1)
                {
                    return $"1 day ago";
                }
                else if(diff.Days > 1)
                {
                    return $"{diff.Days} days ago";
                }
                else if(diff.Hours < 2)
                {
                    return $"{diff.Hours} hour ago";
                }
               
                return $"{diff.Hours} hours ago";
            }
        }

        public DateTime? Time { get; set; }

        public List<MatchViewModel> Matches { get; set; }
    }

    public class MatchViewModel 
    {
        public string IconUrl { get; set; }
        public string Tier { get; set; }
        public string Division { get; set; }
        public string Mode { get; set; }
        public bool Won { get; set; }
        public long Delta { get; set; }
        public long Mmr { get; set; }

        private byte[] _pic;
        public byte[] Pic

        {
            get
            {
                return _pic;
            }
            set
            {
                if (_pic != value)
                {
                    _pic = value;
                }
            }
        }
    }
}