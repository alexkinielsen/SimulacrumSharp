using SimulacrumSharp.SimulationAPI.Models.ServiceModels;

namespace SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation
{
    public interface IDragRaceSimulationService
    {
        DragRaceSimulationResponse Simulate(DragRaceSimulationRequest request);
    }
}
