using SimulacrumSharp.SimulationAPI.Models.Survivor;

namespace SimulacrumSharp.SimulationAPI.Models.ServiceModels
{
    public class SurvivorSimulationResponse
    {
        public string Name { get; set; }
        public IList<Castaway> Castaways { get; set; } = new List<Castaway>();
        public IList<SurvivorEpisode> Episodes { get; set; } = new List<SurvivorEpisode>();
        public string Winner { get; set; }
    }
}
