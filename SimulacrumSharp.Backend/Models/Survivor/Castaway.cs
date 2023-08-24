using System.Text.Json.Serialization;

namespace SimulacrumSharp.Backend.Models.Survivor
{
    public class Castaway
    {
        public string Name { get; internal set; }
        public bool IsImmune { get; internal set; }
        public string Tribe { get; internal set; }
        public int Placement { get; internal set; }
        public bool OnJury { get; internal set; }
        public IDictionary<int, string> TribeHistory { get; internal set; } = new Dictionary<int, string>();
        public IDictionary<string, string> VotingHistory { get; internal set; } = new Dictionary<string, string>();
        public IList<string> WinningVotes { get; internal set; } = new List<string>();
        public IDictionary<string, List<string>> VotesAgainst { get; internal set; } = new Dictionary<string, List<string>>();

    }
}
