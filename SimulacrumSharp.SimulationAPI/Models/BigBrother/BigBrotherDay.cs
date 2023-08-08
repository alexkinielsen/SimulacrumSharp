using System.Collections;

namespace SimulacrumSharp.SimulationAPI.Models.BigBrother
{
    public class BigBrotherDay
    {
        public int DayNumber { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<string> HouseGuestsCompeting { get; set; }
        public ArrayList Events { get; set; }
    }
}