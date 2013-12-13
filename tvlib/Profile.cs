using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvlib
{
    [Serializable()]
    internal class Profile // NYFI
    {
        List<Show> shows;

        public Profile()
        {
            this.shows = new List<Show>();
        }

        public Profile(List<Show> shows)
        {
            this.shows = shows;
        }


        private bool show = true;
        private bool season = false;
        private bool playCount = true;



        public void playEpisode()
        {
            Random r = new Random();
            if (this.show)
            {
                Show s = this.shows[r.Next(shows.Count)];
                if (this.season)
                {
                    Season x = s[r.Next(s.Count)];
                    x.playRandomEpisode(this.playCount);
                }
                else
                {
                    s.PlayRandomEpisode(this.playCount);
                }
            }
            else
            {
                List<Episode> l = new List<Episode>();
                foreach (Show s in this.shows)
                {
                    foreach (Season sea in s)
                    {
                        foreach (Episode e in sea)
                        {
                            l.Add(e);
                        }
                    }
                }
                Show.WeightedRandom(l);
            }
        }



    }
}
