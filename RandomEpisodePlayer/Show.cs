using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RandomEpisodePlayer
{
    class Show : ICollection<Season>
    {
        private Season[] seasons;
        private string name;
        private int len;
        private static Regex reg = new Regex("Season [0-9]*");
        private static Regex num = new Regex("[0-9]");
        


        public Show(string name, int len)
        {
            this.seasons = new Season[len];
            this.len = len;
            this.name = name;
        }

        public Show(string path)
        {
            String[] dirs = Directory.GetDirectories(path);
            foreach (string s in dirs) { if (Show.reg.IsMatch(s)) { this.len++; } }
            this.seasons = new Season[this.len];
            foreach (string s in dirs)
            {
                if (Show.reg.IsMatch(s))
                {
                    int i = Convert.ToInt32(Show.num.Match(s).ToString());
                    this.seasons[i - 1] = new Season(s, i);
                }
            }
        }

        public void Add(Season item)
        {
            this.seasons[item.number] = item;
        }

        public void Clear()
        {
            this.seasons = new Season[len];
        }

        public bool Contains(Season item)
        {
            return this.seasons.Contains(item);
        }

        public void CopyTo(Season[] array, int arrayIndex)
        {
            this.seasons.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return (int) this.len; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Season item)
        {
            if (this.seasons[item.number] == item)
            {
                this.seasons[item.number] = null;
                return true;
            }
            else { return false; }
            
        }

        public IEnumerator<Season> GetEnumerator()
        {
            return (IEnumerator<Season>) this.seasons.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        public void playRandomSeason()
        {
            Random r = new Random();
            playRandomFromSeason(r.Next(this.len));
        }

        public void playRandomFromSeason(int index)
        {
            this.seasons[index].playRandomEpisode();
        }

        public void PlayRandomEpisode()
        {
            List<Episode> l = new List<Episode>();
            foreach (Season s in this)
            {
                foreach (Episode e in s)
                {
                    l.Add(e);
                }
            }
            Random r = new Random();
            l[r.Next(l.Count)].play();
        }

        public Season this[int index]
        {
            get
            {
                return this.seasons[index - 1];
            }
        }

    }
}
