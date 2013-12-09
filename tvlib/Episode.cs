using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace tvlib
{
    class Episode
    {
        private string name;
        private string path;
        private int season;
        private int episode;
        private int playCount;
        private static Regex episodeName = new Regex("S[0-9][1-9]E[0-9][1-9]");
        private Season seasonObj;


        public Episode(string path, Season obj)
        {
            this.path = path;
            Match m = episodeName.Match(this.path);
            String filename = Path.GetFileName(this.path);
            String data = Episode.episodeName.Match(filename).ToString();
            this.season = Convert.ToInt32(data.Substring(1,2));
            this.episode = Convert.ToInt32(data.Substring(5,2));
            this.seasonObj = obj;
        }

        public void play()
        {
            this.playCount++;
            System.Diagnostics.Process.Start(this.path);
        }

        public string ToString()
        {
            return this.seasonObj.name + ".S" + this.season.ToString("00") + "E" + this.episode.ToString("00");            
        }
    }
}
