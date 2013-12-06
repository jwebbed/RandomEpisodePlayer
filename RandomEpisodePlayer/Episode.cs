using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RandomEpisodePlayer
{
    class Episode
    {
        private string name;
        private string path;
        private byte season;
        private byte episode;
        private int playCount;


        public Episode(string path, string name, byte season, byte episode)
        {
            this.path = path;
            this.name = name;
            this.season = season;
            this.episode = episode;
        }

        public void play()
        {
            this.playCount++;
            System.Diagnostics.Process.Start(this.path);
        }
    }
}
