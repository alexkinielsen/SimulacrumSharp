using SimulacrumSharp.Backend.Models.ServiceModels;

namespace SimulacrumSharp.Backend.Services.Interfaces.Simulation.BigBrother
{
    public interface IBigBrotherSimulationService
    {
        BigBrotherSimulationResponse Simulate(BigBrotherSimulationRequest request);
    }
}
