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
    [Route("api/[controller]")]
    public class RunsController : ControllerBase
    {
        private readonly ILogger<RunsController> _logger;
        private readonly IRunService _runService;

        public RunsController(ILogger<RunsController> logger, IRunService runService)
        {
            _logger = logger;
            _runService = runService;
        }

        [HttpGet("metadata")]
        public async Task<Metadata> GetRunMetadata()
        {
            var amountOfRuns = await _runService.GetAmountOfRuns();
            return new Metadata {Count = amountOfRuns};
        }
        
        [HttpGet("current")]
        public async Task<Run> GetCurrentRun()
        {
            return await _runService.GetCurrentRun();
        }
    }
}