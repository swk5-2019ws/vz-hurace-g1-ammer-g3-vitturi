using System.Threading.Tasks;
using Hurace.Api.Models;
using Hurace.Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hurace.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly ILogger<RaceController> _logger;
        private readonly IRaceService _raceService;

        public RaceController(ILogger<RaceController> logger, IRaceService raceService)
        {
            _logger = logger;
            _raceService = raceService;
        }

        [HttpGet("metadata")]
        public async Task<Metadata> GetRaceMetadata()
        {
            var amountOfRaces = await _raceService.GetAmountOfRaces();
            return new Metadata {Count = amountOfRaces};
        }
    }
}