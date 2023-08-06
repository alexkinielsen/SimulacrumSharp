using Microsoft.AspNetCore.Mvc;
using SimulacrumSharp.SimulationAPI.Models.ServiceModels;
using SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation;
using System.Net.Mime;

namespace SimulacrumSharp.SimulationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DragRaceSimulationController : ControllerBase
    {
        private readonly IDragRaceSimulationService _dragRaceSimulationService;

        public DragRaceSimulationController(IDragRaceSimulationService dragRaceSimulationService)
        {
            _dragRaceSimulationService = dragRaceSimulationService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DragRaceSimulationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public ActionResult Simulate(DragRaceSimulationRequest request)
        {
            try
            {
                var response = _dragRaceSimulationService.Simulate(request);

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
