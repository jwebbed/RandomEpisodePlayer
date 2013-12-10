using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace tvlib
{
    internal class Show : ICollection<Season>
    {
        private List<Season> seasons;
        private string _name;
        private int len;
        private static Regex reg = new Regex("Season [1-9][0-9]*");      

        public Show(string path)
        {
            this._name = Path.GetFileName(path);
            String[] dirs = Directory.GetDirectories(path);
            foreach (string s in dirs) { if (Show.reg.IsMatch(s)) { this.len++; } }
            this.seasons = new List<Season>(this.len);
            foreach (string s in dirs)
            {
                if (Show.reg.IsMatch(s))
                {
                    int i = Convert.ToInt32(Show.reg.Match(s).ToString().Substring(7));
                    this.seasons[i - 1] = new Season(s, i, this);
                }
            }
        }

        public void Add(Season item)
        {
            this.seasons[item.number] = item;
        }

        public void Clear()
        {
            this.seasons.Clear();
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
            return this.seasons.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        public void playRandomSeason(bool weighted = false)
        {
            Random r = new Random();
            playRandomFromSeason(r.Next(this.len));
        }

        /// <summary>
        /// Plays a random episode from given season
        /// </summary>
        /// <param name="index">
        /// The number of the seasons to play a random episode from
        /// </param>
        public void playRandomFromSeason(int index)
        {
            this.seasons[index - 1].playRandomEpisode(false);
        }

        public void PlayRandomEpisode(bool weighted)
        {
            int epi = 0;
            foreach (Season s in this)
            {
                epi += s.Count;
            }
            List<Episode> l = new List<Episode>(epi);
            foreach (Season s in this)
            {
                l.AddRange(s.Episodes);
            }
            if (weighted)
            {
                Show.WeightedRandom(l);
            }
            else
            {
                Random r = new Random();
                l[r.Next(epi)].play();
            }
        }

        public Season this[int index]
        {
            get
            {
                if (index < 1)
                {
                    throw new ArgumentOutOfRangeException("index", "It is impossible to have a zero or negative season number");
                }
                return this.seasons[index - 1];
            }
        }

        public string name { get { return this._name; } }

        internal static void WeightedRandom(List<Episode> episodes, double weight = 0.5)
        {
            int max = 0;
            foreach (Episode e in episodes){
                if (e.playCount > max)
                {
                    max = e.playCount;
                }
            }
            int[] plays = new int[max];
            foreach (Episode e in episodes)
            {
                plays[e.playCount]++;
            }
            int i = 0;
            double p = 0;
            while (i < (max + 1))
            {
                p += (plays[i] * (Math.Pow(weight, (double)i)));
                i++;
            }
            p = 1 / p;
            foreach (Episode e in episodes)
            {
                e.setProb(p, weight);
            }
            Random r = new Random();
            double target = r.NextDouble();
            double current = 0;
            foreach (Episode e in episodes)
            {
                current += e.prob;
                if (current > target)
                {
                    e.play();
                    break;
                }
            }
        }
    }
}
