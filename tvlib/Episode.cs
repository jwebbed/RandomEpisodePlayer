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
        private string _path;
        private int seasonNum;
        private int episode;
        private int _playCount;
        private static Regex episodeName = new Regex("S[0-9][1-9]E[0-9][1-9]");
        private Season season;

        public int playCount { get { return this._playCount; } }
        private double _prob;
        public string path { get { return this._path; } }
        public string filename { get { return Path.GetFileName(this._path); } }


        public double prob 
        {   
            get 
            { 
                double x = this._prob;
                this._prob = 0;
                return x; 
            } 
            private set 
            { 
                this._prob = value;
            } 
        }

        public Episode(string path, Season obj)
        {
            this._path = path;
            Match m = episodeName.Match(this._path);
            String filename = Path.GetFileName(this._path);
            String data = Episode.episodeName.Match(filename).ToString();
            /*this.seasonNum = Convert.ToInt32(data.Substring(1,2));
            this.episode = Convert.ToInt32(data.Substring(4,2));*/
            this.season = obj;
        }

        public void play()
        {
            this._playCount++;
            System.Diagnostics.Process.Start(this._path);
        }
      
        
        public override string ToString()
        {
            return this.season.name + ".S" + this.seasonNum.ToString("00") + "E" + this.episode.ToString("00");            
        }

        public void setProb(double b, double weight){
            this.prob = b * Math.Pow(weight, this.playCount);
        }

        public void increasePlayCount()
        {
            this._playCount++;
        }
    }
}
