using SimulacrumSharp.Backend.Models.Survivor;

namespace SimulacrumSharp.Backend.Models.ServiceModels
{
    public class SurvivorSimulationResponse
    {
        public string Name { get; set; }
        public IList<Castaway> Castaways { get; set; } = new List<Castaway>();
        public IList<SurvivorEpisode> Episodes { get; set; } = new List<SurvivorEpisode>();
        public string Winner { get; set; }
    }
}
