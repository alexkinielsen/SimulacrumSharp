namespace SimulacrumSharp.Backend.Models.BigBrother
{
    public class BigBrotherEpisode
    {
        public int EpisodeNumber { get; set; }
        public string Name { get; set; }
        public List<string> HouseGuestsCompeting { get; set; }
        public List<BigBrotherDay> Days { get; set; }
    }
}