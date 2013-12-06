using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEpisodePlayer
{
    class Show : ICollection<Season>
    {
        private List<Season> seasons;
        private string name;


        public Show(string name)
        {
            this.seasons = new List<Season>();
            this.name = name;
        }
public void Add(Season item)
{
 	this.seasons.Add(item);
}

public void Clear()
{
 	this.seasons = new List<Season>();
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
	get { return this.seasons.Count; }
}

public bool IsReadOnly
{
	get { return false; }
}

public bool Remove(Season item)
{
 	return this.seasons.Remove(item);
}

public IEnumerator<Season> GetEnumerator()
{
 	return this.seasons.GetEnumerator();
}

System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
{
 	return (IEnumerator)this.GetEnumerator();
}
}
}
