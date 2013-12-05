using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEpisodePlayer
{
    class Season : IEnumerable<Episode>
    {
        private int number;
        private List<Episode> episodes;

        List<Episode> Episodes{ get { return this.episodes; } }

        public Season(int number){
            this.number = number;
            this.episodes = new List<Episode>();
        }

        public Season(int number, List<Episode> list)
        {
            this.number = number;
            this.episodes = list;
        }

        public void addEpisode(Episode e)
        {
            this.episodes.Add(e);
        }

        public IEnumerator<Episode> GetEnumerator()
        {
            return this.episodes.GetEnumerator();
        }
        
    }
}
