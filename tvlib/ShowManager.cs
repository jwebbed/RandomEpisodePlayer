using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace tvlib
{
    [Serializable()]
    public class ShowManager
    {
        private string basedir;


        private Dictionary<string, Show> shows;

        public ShowManager(string basedir)
        {
            this.basedir = basedir;
            this.shows = new Dictionary<string, Show>();
        }

        public void Populate()
        {
            String[] folders = Directory.GetDirectories(this.basedir);
            Show x = new Show(folders[7]);
            this.addShow(x);
        }

        public void playRandomEpisode(string show, bool weighted = true)
        {
            Show s = this.shows[show];
            s.PlayRandomEpisode(weighted);
        }

        public void addShow(Show s)
        {
            this.shows.Add(s.name, s);
        }
    }
}
