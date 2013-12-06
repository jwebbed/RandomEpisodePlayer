using System;
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
        Regex regex = new Regex("S[0-9][0-9]E[0-9][0-9]");

        public ShowManager(string basedir)
        {
            this.basedir = basedir;
        }
    }
}
