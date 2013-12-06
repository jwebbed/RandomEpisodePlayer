﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEpisodePlayer
{
    class Season : ICollection<Episode>
    {
        private int number;
        private List<Episode> episodes;
        private int length;

        List<Episode> Episodes { get { return this.episodes; } }

        public Season(int number, int length)
        {
            this.number = number;
            this.episodes = new List<Episode>();
            this.length = length;
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
    }
}
