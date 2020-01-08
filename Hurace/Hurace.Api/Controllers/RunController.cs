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
    public class RunController : ControllerBase
    {
        private readonly ILogger<RunController> _logger;
        private readonly IRunService _runService;

        public RunController(ILogger<RunController> logger, IRunService runService)
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
    }
}