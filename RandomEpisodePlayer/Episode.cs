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

        public Episode(string path)
        {
            this.path = path;
        }

        public Episode(string path, string name)
        {
            this.path = path;
            this.name = name;
        }

        public void play()
        {
            System.Diagnostics.Process.Start(this.path);
        }
    }
}
