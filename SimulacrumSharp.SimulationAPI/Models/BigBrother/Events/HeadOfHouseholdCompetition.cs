using SimulacrumSharp.SimulationAPI.Models.BigBrother.Events.Base;

namespace SimulacrumSharp.SimulationAPI.Models.BigBrother.Events
{
    public class HeadOfHouseholdCompetition : BigBrotherEvent
    {
        public List<string> Winners { get; set; } = new List<string>();
    }
}
