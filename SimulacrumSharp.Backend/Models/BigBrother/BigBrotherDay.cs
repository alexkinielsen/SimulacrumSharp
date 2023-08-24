using System.Collections;

namespace SimulacrumSharp.Backend.Models.BigBrother
{
    public class BigBrotherDay
    {
        public int DayNumber { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<string> HouseGuestsCompeting { get; set; }
        public ArrayList Events { get; set; }
    }
}