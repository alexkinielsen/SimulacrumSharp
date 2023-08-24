using SimulacrumSharp.Backend.Models.BigBrother.Events.Base;

namespace SimulacrumSharp.Backend.Models.BigBrother.Events
{
    public class HeadOfHouseholdCompetition : BigBrotherEvent
    {
        public List<string> Winners { get; set; } = new List<string>();
    }
}
