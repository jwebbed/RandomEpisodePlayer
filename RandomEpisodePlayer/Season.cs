﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RandomEpisodePlayer
{
    class Season : ICollection<Episode>
    {
        private int _number;
        private List<Episode> episodes;
        private int length;
        private string path;
        private static Regex num = new Regex("[0-9]*");

        public int number { get { return this._number; } }

        List<Episode> Episodes { get { return this.episodes; } }

        public Season(int number, int length)
        {
            this._number = number;
            
            this.length = length;
        }

        public Season(string path, int num)
        {
            this.episodes = new List<Episode>();
            this.path = path;
            this._number = num;
            String[] episodes = Directory.GetFiles(this.path);
            foreach (String e in episodes)
            {
                this.episodes.Add(new Episode(e));
                this.length++;
            }

        }

        public IEnumerator<Episode> GetEnumerator()
        {
            return this.episodes.GetEnumerator();
        }

        public void Add(Episode item)
        {
            this.episodes.Add(item);
        }

        public void Clear()
        {
            this.episodes = new List<Episode>();
        }

        public bool Contains(Episode item)
        {
            return this.episodes.Contains(item);
        }

        public void CopyTo(Episode[] array, int arrayIndex)
        {
            this.episodes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.length; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Episode item)
        {
            return this.episodes.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        public void playRandomEpisode(){
            Random r = new Random();
            this.playEpisode(r.Next(this.length));

        }

        public void playEpisode(int index)
        {
            this.episodes[index].play();
        }
    }
}
