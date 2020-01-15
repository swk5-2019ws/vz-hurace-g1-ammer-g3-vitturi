using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRunService _runService;

        public RunsController(ILogger<RunsController> logger, IRunService runService)
        {
            _logger = logger;
            _runService = runService;
        }

        [HttpGet(Name = "GetAllRuns")]
        public async Task<ActionResult<IEnumerable<Run>>> GetAllRuns([FromQuery] int skierId, [FromQuery] uint season)
        {
            if (skierId == 0 || season == 0) return BadRequest();

            return Ok(await _runService.GetRunsForSkierInSeasons(skierId, season));
        }
        
        [HttpGet("{id}", Name = "GetRunById")]
        public async Task<ActionResult<Run>> GetRunById(int id)
        {
            var run = await _runService.GetRun(id);

            if (run == null) return NotFound();

            return run;
        }

        [HttpGet("metadata", Name = "GetRunMetadata")]
        public async Task<Metadata> GetRunMetadata()
        {
            var amountOfRuns = await _runService.GetAmountOfRuns();
            return new Metadata {Count = amountOfRuns};
        }

        [HttpGet("current", Name = "GetCurrentRun")]
        public async Task<ActionResult<Run>> GetCurrentRun()
        {
            var currentRun = await _runService.GetCurrentRun();

            if (currentRun == null) return NoContent();

            return currentRun;
        }

        [HttpGet("{id}/leaderboard/{runNumber}", Name = "GetLeaderboard")]
        public async Task<ActionResult<IEnumerable<Run>>> GetLeaderboard(int id, int runNumber)
        {
            var run = await _runService.GetRun(id);

            if (run == null) return NotFound();

            return Ok(await _runService.GetLeaderBoard(run.Race, runNumber));
        }

        [HttpGet("{id}/interim", Name = "GetInterimTimes")]
        public async Task<ActionResult<List<string>>> GetInterimTimes(int id)
        {
            var run = await _runService.GetRun(id);

            if (run == null) return NotFound();

            return Ok((await _runService.GetInterimTimes(run.Race, run.RunNumber, run.Skier)).Select(time => time.ToString()));
        }
    }
}