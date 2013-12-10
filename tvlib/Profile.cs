using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvlib
{


    internal class Profile
    {
        // inc lazy naming

        List<Show> shows;

        public Profile()
        {

        }


        private bool show = true;
        private bool season = false;
        private bool playCount = true;



        public void play()
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

            }
        }



    }
}
