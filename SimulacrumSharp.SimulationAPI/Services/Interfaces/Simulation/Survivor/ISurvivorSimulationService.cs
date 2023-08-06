using SimulacrumSharp.SimulationAPI.Models.ServiceModels;

namespace SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation.Survivor
{
    public interface ISurvivorSimulationService
    {
        SurvivorSimulationResponse Simulate(SurvivorSimulationRequest request);
    }
}
