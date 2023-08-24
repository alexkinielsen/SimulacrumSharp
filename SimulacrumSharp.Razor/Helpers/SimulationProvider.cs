using Newtonsoft.Json;
using SimulacrumSharp.Backend.Models.BigBrother;
using SimulacrumSharp.Backend.Models.DragRace;
using SimulacrumSharp.Backend.Models.ServiceModels;
using SimulacrumSharp.Backend.Models.Survivor;
using SimulacrumSharp.Backend.Services.Interfaces;
using SimulacrumSharp.Razor.Helpers.Interfaces;

namespace SimulacrumSharp.Razor.Helpers
{
    public class SimulationProvider : ISimulationProvider
    {
        private readonly ISimulationService<SurvivorSeason> _survivorSimulationService;
        private readonly ISimulationService<DragRaceSeason> _dragRaceSimulationService;
        private readonly ISimulationService<BigBrotherSeason> _bigBrotherSimulationService;

        public SimulationProvider(
            ISimulationService<SurvivorSeason> survivorSimulationService,
            ISimulationService<DragRaceSeason> dragRaceSimulationService,
            ISimulationService<BigBrotherSeason> bigBrotherSimulationService)
        {
            _survivorSimulationService = survivorSimulationService;
            _dragRaceSimulationService = dragRaceSimulationService;
            _bigBrotherSimulationService = bigBrotherSimulationService;
        }
        public string GetSimulation<T>(SimulationRequest<T> simulationRequest)
        {
            try
            {
                var response = FetchResponseFromSimulationService<T>(simulationRequest);

                if (response != null)
                {
                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return "An unknown error occurred.";
                }
            }
            catch (NotImplementedException)
            {
                return "This simulator has not yet been implemented. Check back later!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private object FetchResponseFromSimulationService<T>(SimulationRequest<T> simulationRequest)
        {
            switch (typeof(T).Name)
            {
                case nameof(SurvivorSeason):
                    return _survivorSimulationService.Simulate(simulationRequest as SimulationRequest<SurvivorSeason>);
                case nameof(DragRaceSeason):
                    return _dragRaceSimulationService.Simulate(simulationRequest as SimulationRequest<DragRaceSeason>);
                case nameof(BigBrotherSeason):
                    return _bigBrotherSimulationService.Simulate(simulationRequest as SimulationRequest<BigBrotherSeason>);
                default:
                    return new();
            }
        }
    }
}
