using SimulacrumSharp.SimulationAPI.Models.BigBrother;

namespace SimulacrumSharp.SimulationAPI.Models.ServiceModels
{
    public class BigBrotherSimulationResponse
    {
        public List<HouseGuest> HouseGuests { get; set; }
        public string Winner { get; set; }
        public string AmericasFavoritePlayer { get; set; }
        public List<BigBrotherEpisode> Episodes { get; set; }
    }
}
