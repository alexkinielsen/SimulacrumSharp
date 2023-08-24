using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacrumSharp.Backend.Models.Survivor
{
    public class SurvivorSeason
    {
        public string Name { get; set; }
        public IList<Castaway> Castaways { get; set; } = new List<Castaway>();
        public IList<SurvivorEpisode> Episodes { get; set; } = new List<SurvivorEpisode>();
        public string Winner { get; set; }
    }
}
