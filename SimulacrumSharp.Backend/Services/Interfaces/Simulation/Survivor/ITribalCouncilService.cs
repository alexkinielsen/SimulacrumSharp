using SimulacrumSharp.Backend.Models.Survivor;

namespace SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor
{
    public interface ITribalCouncilService
    {
        TribalCouncil SimulateTribalCouncil(SurvivorEpisode episode, int day, IList<Castaway> castawaysRemaining, Tribe attendingTribe, bool addToJury);
        TribalCouncil SimulateFinalTribalCouncil(SurvivorEpisode episode, int day, IList<Castaway> finalists, IList<Castaway> jury);
    }
}