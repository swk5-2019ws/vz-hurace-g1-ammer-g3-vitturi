using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Hurace.Api.Models;
using Hurace.Core.Helper;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Microsoft.AspNetCore.Http;
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
        private readonly IRunService _runService;

        public RacesController(ILogger<RacesController> logger, IRaceService raceService, IRunService runService)
        {
            _logger = logger;
            _raceService = raceService;
            _runService = runService;
        }

        [HttpGet("metadata", Name = "GetRaceMetadata")]
        public async Task<Metadata> GetRaceMetadata()
        {
            var amountOfRaces = await _raceService.GetAmountOfRaces();
            return new Metadata {Count = amountOfRaces};
        }

        [HttpGet(Name = "GetAllRaces")]
        public async Task<ActionResult<IEnumerable<Race>>> GetAllRaces([FromQuery] uint season, [FromQuery] int locationId)
        {
            if (season == 0 || locationId == 0) return BadRequest();

            var races = (await _raceService.GetRaces()).Where(r =>
                r.Location.Id == locationId && r.Date > SeasonParser.GetSeasonsStart(season) &&
                r.Date < SeasonParser.GetSeasonsEnd(season));
            return Ok(races);
        }

        [HttpGet("current", Name = "GetCurrentRace")]
        public async Task<ActionResult<Race>> GetCurrentRace()
        {
            var currentRace = await _raceService.GetCurrentRace();

            if (currentRace == null) return NoContent();

            return currentRace;
        }
        
        [HttpGet("open/{id}", Name = "GetOpenRacesForSkier")]
        public async Task<IEnumerable<Race>> GetOpenRacesForSkier(int id)
        {
            var openRaces = (await _raceService.GetRaces()).Where(race => race.Status == RaceStatus.Ready);
            return openRaces.Where(race => _runService.GetAllRunsForRace(race, 1).Result.All(run => run.Skier.Id != id));
        }
        
        [HttpGet("closed/{id}", Name = "GetClosedRacesForSkier")]
        public async Task<IEnumerable<Race>> GetClosedRacesForSkier(int id)
        {
            var closedRaces = (await _raceService.GetRaces()).Where(race => race.Status == RaceStatus.InProgress || race.Status == RaceStatus.Finished);
            return closedRaces.Where(race => _runService.GetAllRunsForRace(race, 1).Result.Any(run => run.Skier.Id == id));
        }
        
        [HttpGet("{id}/runs", Name = "GetRunsForRace")]
        public async Task<ActionResult<IEnumerable<Run>>> GetRunsForRace(int id, [FromQuery] int runNumber)
        {
            var race = await _raceService.GetRace(id);
            
            if (race == null || race.Status == RaceStatus.Ready) return BadRequest();

            return Ok(await _runService.GetAllRunsForRace(race, runNumber));
        }

        [HttpPost("{id}/runs", Name = "AddRunToRace")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Run>> AddRunToRace(int id,
            [CustomizeValidator(RuleSet = "CreateSkierValidation")] [FromBody]
            Skier skier)
        {
            var race = await _raceService.GetRace(id);

            if (race == null || race.Status != RaceStatus.Ready) return BadRequest();

            var runs = await _runService.GetAllRunsForRace(race, 1);
            var skiers = runs.Select(run => run.Skier).ToList();
            skiers.Add(skier);
            await _raceService.EditStartList(race, 1, skiers);

            var insertedRun = (await _runService.GetAllRunsForRace(race, 1)).LastOrDefault();

            return CreatedAtRoute("GetRunById", new {id = insertedRun.Id}, insertedRun);
        }
    }
}