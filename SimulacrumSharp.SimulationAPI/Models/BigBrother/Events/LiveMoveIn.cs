using SimulacrumSharp.SimulationAPI.Models.BigBrother.Events.Base;
using SimulacrumSharp.SimulationAPI.Models.Enums;

namespace SimulacrumSharp.SimulationAPI.Models.BigBrother.Events
{
    public class MultiverseMoveIn : BigBrotherEvent
    {
        public Dictionary<int, List<string>> MoveInGroups { get; set; }
        public Dictionary<string, List<string>> ChallengeStations { get; set; }
    }
}