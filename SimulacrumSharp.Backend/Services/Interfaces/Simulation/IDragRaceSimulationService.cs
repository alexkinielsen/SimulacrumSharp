using SimulacrumSharp.Backend.Models.ServiceModels;

namespace SimulacrumSharp.Backend.Services.Interfaces.Simulation
{
    public interface IDragRaceSimulationService
    {
        DragRaceSimulationResponse Simulate(DragRaceSimulationRequest request);
    }
}
