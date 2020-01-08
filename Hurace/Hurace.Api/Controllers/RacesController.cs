using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Api.Models;
using Hurace.Core.Helper;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hurace.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class RacesController : ControllerBase
    {
        private readonly ILogger<RacesController> _logger;
        private readonly IRaceService _raceService;

        public RacesController(ILogger<RacesController> logger, IRaceService raceService)
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetAll([FromQuery] uint season, [FromQuery] int locationId)
        {
            if (season == 0 || locationId == 0) return BadRequest();
            
            var races = (await _raceService.GetRaces()).Where(r =>
                r.Location.Id == locationId && r.Date > SeasonParser.GetSeasonsStart(season) &&
                r.Date < SeasonParser.GetSeasonsEnd(season));
            return Ok(races);
        }
    }
}