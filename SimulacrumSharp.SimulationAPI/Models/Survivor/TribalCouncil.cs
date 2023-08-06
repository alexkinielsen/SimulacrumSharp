namespace SimulacrumSharp.SimulationAPI.Models.Survivor
{
    public class TribalCouncil
    {
        public string Name { get; internal set; }
        public int EpisodeNumber { get; internal set; }
        public int Day { get; internal set; }
        public string AttendingTribe { get; internal set; }

        public IList<string> CastawaysAttending { get; internal set; } = new List<string>();

        public IList<string> AttendeesWithImmunity { get; internal set; } = new List<string>();
        public IList<VotingRound> VotingRounds { get; internal set; } = new List<VotingRound>();
        public string VotedOut { get; internal set; }
        public IList<string> CastawaysRemaining { get; set; } = new List<string>();
        public string SoleSurvivor { get; set; }
    }
}
