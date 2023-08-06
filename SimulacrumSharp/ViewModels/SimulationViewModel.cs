using SimulacrumSharp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacrumSharp.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        private readonly ISimulacrumApiService _simulacrumApiService;

        public SimulationViewModel()
        {

        }
        public SimulationViewModel(ISimulacrumApiService simulacrumApiService)
        {
            _simulacrumApiService = simulacrumApiService;
        }
    }
}
