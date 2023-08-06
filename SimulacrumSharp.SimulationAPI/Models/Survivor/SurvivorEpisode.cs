namespace SimulacrumSharp.SimulationAPI.Models.Survivor
{
    public class SurvivorEpisode
    {
        public int EpisodeNumber { get; internal set; }
        public string Name { get; internal set; }
        public IList<string> CastawaysCompeting { get; internal set; } = new List<string>();
        public IList<TribalCouncil> TribalCouncils { get; internal set; } = new List<TribalCouncil>();
        public IList<string> EpisodeNotes { get; internal set; } = new List<string>();
    }
}
