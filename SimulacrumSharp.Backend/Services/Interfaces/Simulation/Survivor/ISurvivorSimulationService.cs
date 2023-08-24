using SimulacrumSharp.Backend.Models.ServiceModels;

namespace SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor
{
    public interface ISurvivorSimulationService
    {
        SurvivorSimulationResponse Simulate(SurvivorSimulationRequest request);
    }
}
