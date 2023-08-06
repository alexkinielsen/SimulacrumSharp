using SimulacrumSharp.SimulationAPI.Models.ServiceModels;

namespace SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation.BigBrother
{
    public interface IBigBrotherSimulationService
    {
        BigBrotherSimulationResponse Simulate(BigBrotherSimulationRequest request);
    }
}
