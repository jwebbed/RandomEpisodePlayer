using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvlib
{

    
    internal class Profile
    {
        private equality equality;


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
