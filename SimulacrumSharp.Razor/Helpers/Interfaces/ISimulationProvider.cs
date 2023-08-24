using SimulacrumSharp.Backend.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacrumSharp.Razor.Helpers.Interfaces
{
    public interface ISimulationProvider
    {
        string GetSimulation<T>(SimulationRequest<T> request);
    }
}
