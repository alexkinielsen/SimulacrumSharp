using SimulacrumSharp.Backend.Models.BigBrother.Events.Base;
using SimulacrumSharp.Backend.Models.Enums;

namespace SimulacrumSharp.Backend.Models.BigBrother.Events
{
    public class MultiverseMoveIn : BigBrotherEvent
    {
        public Dictionary<int, List<string>> MoveInGroups { get; set; }
        public Dictionary<string, List<string>> ChallengeStations { get; set; }
    }
}