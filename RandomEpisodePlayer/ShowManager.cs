﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RandomEpisodePlayer
{
    class ShowManager
    {
        private string basedir;
        
        
        private List<Show> shows;

        public ShowManager(string basedir)
        {
            this.basedir = basedir;
            this.shows = new List<Show>();
        }

        public void Populate()
        {
            String[] folders = Directory.GetDirectories(this.basedir);
            shows.Add(new Show(folders[8]));
        }
    }
}
