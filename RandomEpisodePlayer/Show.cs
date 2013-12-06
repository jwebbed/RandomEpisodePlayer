using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEpisodePlayer
{
    class Show : ICollection<Season>
    {
        private Season[] seasons;
        private string name;
        private byte len;


        public Show(string name, byte len)
        {
            this.seasons = new Season[len];
            this.len = len;
            this.name = name;
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
    }
}
