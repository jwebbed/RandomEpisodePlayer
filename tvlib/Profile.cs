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
        private equality meh;
        List<Show> shows;

        public Profile()
        {
            this.meh = new equality(true, false, true);
        }

        private struct equality
        {
            private bool show;
            private bool season;
            private bool playCount;

            public equality(bool show, bool season, bool play)
            {
                this.show = show;
                this.season = season;
                this.playCount = play;
            }

      
            
        }  
    }

    
}
