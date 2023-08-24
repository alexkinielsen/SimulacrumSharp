using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacrumSharp.Backend.Models.BigBrother
{
    public class BigBrotherSeason
    {
        public List<HouseGuest> HouseGuests { get; set; }
        public string Winner { get; set; }
        public string AmericasFavoritePlayer { get; set; }
        public List<BigBrotherEpisode> Episodes { get; set; }
    }
}
