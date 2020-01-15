using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Hurace.Api.Models;
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
    public class SkiersController : ControllerBase
    {
        private readonly ILogger<SkiersController> _logger;
        private readonly ISkierService _skierService;

        public SkiersController(ILogger<SkiersController> logger, ISkierService skierService)
        {
            _logger = logger;
            _skierService = skierService;
        }

        [HttpGet(Name = "GetAllSkiers")]
        public async Task<IEnumerable<Skier>> GetAllSkiers([FromQuery] string name, [FromQuery] string gender)
        {
            Gender? genderObject = null;
            switch (gender)
            {
                case "male":
                    genderObject = Gender.Male;
                    break;
                case "female":
                    genderObject = Gender.Female;
                    break;
            }

            return await _skierService.GetSkiers(genderObject, name);
        }

        [HttpGet("{id}", Name = "GetSkierById")]
        public async Task<ActionResult<Skier>> GetSkierById(int id)
        {
            var skier = await _skierService.GetSkier(id);

            if (skier == null) return NotFound();

            return skier;
        }

        [HttpGet("metadata", Name = "GetSkierMetadata")]
        public async Task<Metadata> GetSkierMetadata()
        {
            var amountOfSkiers = await _skierService.GetAmountOfSkiers();
            return new Metadata {Count = amountOfSkiers};
        }

        [HttpPost(Name = "CreateSkier")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Skier>> CreateSkier([CustomizeValidator(RuleSet = "CreateSkierValidation")]
            Skier skier)
        {
            var insertedSkier = await _skierService.CreateSkier(skier);
            return CreatedAtRoute("GetSkierById", new {id = insertedSkier.Id}, insertedSkier);
        }

        [HttpPut("{id}", Name = "UpdateSkier")]
        public async Task<IActionResult> UpdateSkier(int id, [CustomizeValidator(RuleSet = "*")] [FromBody]
            Skier item)
        {
            if (item == null || item.Id != id) return BadRequest();

            var skier = await _skierService.GetSkier(id);

            if (skier == null) return NotFound();

            await _skierService.UpdateSkier(item);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteSkier")]
        public async Task<IActionResult> DeleteSkier(int id)
        {
            var skier = await _skierService.GetSkier(id);

            if (skier == null) return NotFound();

            await _skierService.RemoveSkier(skier.Id);
            return NoContent();
        }
    }
}