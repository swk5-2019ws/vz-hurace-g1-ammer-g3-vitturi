using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Api.Models;
using Hurace.Core.Helper;
using Hurace.Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hurace.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class SeasonsController : ControllerBase
    {
        private readonly ILogger<SeasonsController> _logger;
        private readonly IRaceService _raceService;
        private ILocationService _locationService;

        public SeasonsController(ILogger<SeasonsController> logger, IRaceService raceService, ILocationService locationService)
        {
            _logger = logger;
            _raceService = raceService;
            _locationService = locationService;
        }

        [HttpGet(Name = "GetAllSeasons")]
        public async Task<IEnumerable<int>> GetAllSeasons()
        {
            return (await _raceService.GetRaces()).Select(r => r.Date.Year).Distinct();
        }   
        
        [HttpGet("{season}", Name = "GetSeason")]
        public async Task<ActionResult<Season>> GetSeason(uint season)
        {
            var skiSeason = new Season {Year = Convert.ToDateTime($"01.01.{season}")};
            var races = (await _raceService.GetRaces()).Where(r =>
                r.Date > SeasonParser.GetSeasonsStart(season) && r.Date < SeasonParser.GetSeasonsEnd(season));

            if (!races.Any()) return NoContent();

            List<SkiEvent> events = new List<SkiEvent>();
            foreach (var grouping in races.GroupBy(r => r.Location.Id))
            {
                var skiEvent = new SkiEvent();
                skiEvent.Location = await _locationService.GetLocation(grouping.Key);
                skiEvent.StartDate = grouping.Min(r => r.Date);
                skiEvent.EndDate = grouping.Max(r => r.Date);
                skiEvent.RaceTypes = grouping.Select(r => r.RaceType).Distinct();
                skiEvent.Genders = grouping.Select(r => r.Gender).Distinct();
                events.Add(skiEvent);
            }
            skiSeason.Events = events;

            return Ok(skiSeason);
        }
    }
}