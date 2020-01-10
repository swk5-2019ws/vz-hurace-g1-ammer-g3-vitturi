using System.Collections.Generic;
using System.Threading.Tasks;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hurace.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly ICountryService _countryService;

        public CountriesController(ILogger<CountriesController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _countryService.GetCountries();
        }
    }
}