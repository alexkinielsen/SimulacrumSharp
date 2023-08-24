using SimulacrumSharp.Backend.Models.DragRace;
using SimulacrumSharp.Backend.Models.ServiceModels;
using SimulacrumSharp.Backend.Services.Interfaces;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation;

namespace SimulacrumSharp.Backend.Services.Simulation.DragRace
{
    public class DragRaceSimulationService : ISimulationService<DragRaceSeason>
    {
        public SimulationResponse<DragRaceSeason> Simulate(SimulationRequest<DragRaceSeason> request)
        {
            throw new NotImplementedException();
        }
    }
}
