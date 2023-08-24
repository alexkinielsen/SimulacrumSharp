using SimulacrumSharp.Backend.Models.ServiceModels;

namespace SimulacrumSharp.Backend.Services.Interfaces
{
    public interface ISimulationService<T>
    {
        SimulationResponse<T> Simulate(SimulationRequest<T> request);
    }
}
