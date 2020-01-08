using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Api.Models;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hurace.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class RunsController : ControllerBase
    {
        private readonly ILogger<RunsController> _logger;
        private readonly IRaceService _raceService;
        private readonly IRunService _runService;

        public RunsController(ILogger<RunsController> logger, IRunService runService, IRaceService raceService)
        {
            _logger = logger;
            _raceService = raceService;
            _runService = runService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Run>>> GetAll([FromQuery] int skierId, [FromQuery] uint season)
        {
            if (skierId == 0 || season == 0) return BadRequest();

            return Ok(await _runService.GetRunsForSkierInSeasons(skierId, season));
        }

        [HttpGet("metadata")]
        public async Task<Metadata> GetRunMetadata()
        {
            var amountOfRuns = await _runService.GetAmountOfRuns();
            return new Metadata {Count = amountOfRuns};
        }

        [HttpGet("current")]
        public async Task<ActionResult<Run>> GetCurrentRun()
        {
            var currentRun = await _runService.GetCurrentRun();

            if (currentRun == null) return NoContent();

            return currentRun;
        }

        [HttpGet("{id}/leaderboard/{runNumber}")]
        public async Task<ActionResult<IEnumerable<Run>>> GetLeaderboard(int id, int runNumber)
        {
            var run = await _runService.GetRun(id);

            if (run == null) return NotFound();

            return Ok(await _runService.GetLeaderBoard(run.Race, runNumber));
        }

        [HttpGet("{id}/interim")]
        public async Task<ActionResult<List<TimeSpan>>> GetInterimTimes(int id)
        {
            var run = await _runService.GetRun(id);

            if (run == null) return NotFound();

            return Ok(await _runService.GetInterimTimes(run.Race, run.RunNumber, run.Skier));
        }
    }
}