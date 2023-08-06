namespace SimulacrumSharp.SimulationAPI.Models.Survivor
{
    public class VotingRound
    {
        public IList<string> MostVotedCastaways { get; set; } = new List<string>();
        public IDictionary<string, string> Votes { get; set; } = new Dictionary<string, string>();
        public string Name { get; set; }
    }
}