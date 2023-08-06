namespace SimulacrumSharp.SimulationAPI.Models.Survivor
{
    public class Tribe
    {
        public string Name { get; set; }
        public bool IsMergeTribe { get; set; }
        public bool IsImmune { get; set; }
        public IList<string> Castaways { get; set; } = new List<string>();
    }
}
