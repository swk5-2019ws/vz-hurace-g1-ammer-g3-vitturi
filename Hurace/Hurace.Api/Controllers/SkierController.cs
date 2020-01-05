using System.Threading.Tasks;
using Hurace.Api.Models;
using Hurace.Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hurace.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class SkierController : ControllerBase
    {
        private readonly ILogger<SkierController> _logger;
        private readonly ISkierService _skierService;

        public SkierController(ILogger<SkierController> logger, ISkierService skierService)
        {
            _logger = logger;
            _skierService = skierService;
        }

        [HttpGet("metadata")]
        public async Task<Metadata> GetSkierMetadata()
        {
            var amountOfSkiers = await _skierService.GetAmountOfSkiers();
            return new Metadata {Count = amountOfSkiers};
        }
    }
}