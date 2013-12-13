using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace tvlib
{
    [Serializable()]
    public class Episode
    {
        private string name;
        private string path;
        private int seasonNum;
        private int episode;
        private int _playCount;
        private static Regex episodeName = new Regex("S[0-9][1-9]E[0-9][1-9]");
        private Season season;

        public int playCount { get { return this._playCount; } }
        public double prob { get; private set; }
        public Episode(string path, Season obj)
        {
            this.path = path;
            Match m = episodeName.Match(this.path);
            String filename = Path.GetFileName(this.path);
            String data = Episode.episodeName.Match(filename).ToString();
            /*this.seasonNum = Convert.ToInt32(data.Substring(1,2));
            this.episode = Convert.ToInt32(data.Substring(4,2));*/
            this.season = obj;
        }

        public void play()
        {
            this._playCount++;
            System.Diagnostics.Process.Start(this.path);
        }
      
        
        public override string ToString()
        {
            return this.season.name + ".S" + this.seasonNum.ToString("00") + "E" + this.episode.ToString("00");            
        }

        public void setProb(double b, double weight){
            this.prob = b * Math.Pow(weight, this.playCount);
        }
    }
}
