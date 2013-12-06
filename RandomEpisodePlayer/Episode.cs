using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RandomEpisodePlayer
{
    class Episode
    {
        private string name;
        private string path;
        private int season;
        private int episode;
        private int playCount;
        private static Regex episodeName = new Regex("S[0-9][0-9]E[0-9][0-9]");


        public Episode(string path)
        {
            this.path = path;
            Match m = episodeName.Match(this.path);
            
        }

        public void play()
        {
            this.playCount++;
            System.Diagnostics.Process.Start(this.path);
        }
    }
}
