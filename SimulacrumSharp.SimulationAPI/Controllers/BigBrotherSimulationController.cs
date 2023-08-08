using Microsoft.AspNetCore.Mvc;
using SimulacrumSharp.SimulationAPI.Models.ServiceModels;
using SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation.BigBrother;
using System.Net.Mime;

namespace SimulacrumSharp.SimulationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BigBrotherSimulationController : ControllerBase
    {
        private readonly IBigBrotherSimulationService _bigBrotherSimulationService;

        public BigBrotherSimulationController(IBigBrotherSimulationService bigBrotherSimulationService)
        {
            _bigBrotherSimulationService = bigBrotherSimulationService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BigBrotherSimulationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public ActionResult Simulate(BigBrotherSimulationRequest request)
        {
            try
            {
                var response = _bigBrotherSimulationService.Simulate(request);

                return Ok(response);
            }
            catch (NotImplementedException exception)
            {
                Console.WriteLine($"Log Error: {exception.Message}"); //TODO: Replace with logger
                return StatusCode(StatusCodes.Status405MethodNotAllowed,
                    "This simulator has not yet been implemented. Check back later!");
            }
        }
    }
}

