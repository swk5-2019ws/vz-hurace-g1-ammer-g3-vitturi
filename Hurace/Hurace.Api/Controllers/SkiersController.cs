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
    [Route("api/[controller]")]
    public class SkiersController : ControllerBase
    {
        private readonly ILogger<SkiersController> _logger;
        private readonly ISkierService _skierService;

        public SkiersController(ILogger<SkiersController> logger, ISkierService skierService)
        {
            _logger = logger;
            _skierService = skierService;
        }

        [HttpGet]
        public async Task<IEnumerable<Skier>> GetAll([FromQuery] string name, [FromQuery] string gender)
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
        
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Skier>> GetById(int id)
        {
            var skier = await _skierService.GetSkier(id);
            
            if (skier == null)
            {
                return NotFound();
            }

            return skier;
        }
        
        [HttpGet("metadata")]
        public async Task<Metadata> GetSkierMetadata()
        {
            var amountOfSkiers = await _skierService.GetAmountOfSkiers();
            return new Metadata {Count = amountOfSkiers};
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Skier>> Create([CustomizeValidator(RuleSet="CreateSkierValidation")] Skier skier)
        {
            var insertedSkier = await _skierService.CreateSkier(skier);
            return CreatedAtRoute("GetById", new { id = insertedSkier.Id }, insertedSkier);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [CustomizeValidator(RuleSet="*")][FromBody] Skier item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var skier = await _skierService.GetSkier(id);

            if (skier == null)
            {
                return NotFound();
            }

            await _skierService.UpdateSkier(item);
            
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var skier = await _skierService.GetSkier(id);

            if (skier == null)
            {
                return NotFound();
            }

            await _skierService.RemoveSkier(skier.Id);
            return NoContent();
        }
    }
}