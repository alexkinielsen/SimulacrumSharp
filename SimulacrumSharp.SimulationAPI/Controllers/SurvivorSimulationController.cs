using Microsoft.AspNetCore.Mvc;
using SimulacrumSharp.SimulationAPI.Models.ServiceModels;
using SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation.Survivor;
using System.Net.Mime;

namespace SimulacrumSharp.SimulationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurvivorSimulationController : ControllerBase 
    {
        private readonly ISurvivorSimulationService _survivorSimulationService;

        public SurvivorSimulationController(ISurvivorSimulationService survivorSimulationService)
        {
            _survivorSimulationService = survivorSimulationService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SurvivorSimulationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public ActionResult Simulate(SurvivorSimulationRequest request)
        {
            try
            {
                var response = _survivorSimulationService.Simulate(request);

                return Ok(response);
            }
            catch(NotImplementedException exception)
            {
                Console.WriteLine($"Log Error: {exception.Message}"); //TODO: Replace with logger
                return StatusCode(StatusCodes.Status405MethodNotAllowed,
                    "This simulator has not yet been implemented. Check back later!");
            }
        }
    }
}
