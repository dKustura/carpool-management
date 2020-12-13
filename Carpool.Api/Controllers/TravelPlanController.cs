using Carpool.Core.Exceptions;
using Carpool.Core.Requests;
using Carpool.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPlanController : ControllerBase
    {
        private readonly ITravelPlanService _travelPlanService;
        public TravelPlanController(ITravelPlanService travelPlanService)
        {
            _travelPlanService = travelPlanService ?? throw new ArgumentNullException(nameof(travelPlanService));
        }

        // GET: api/<TravelPlanController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _travelPlanService.GetAll();
            return Ok(result);
        }

        // GET api/<TravelPlanController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _travelPlanService.GetById(id);
                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            } catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/<TravelPlanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TravelPlanCreateRequest request)
        {
            var model = await _travelPlanService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = model.TravelPlanId }, null);
        }

        // PUT api/<TravelPlanController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] TravelPlanUpdateRequest request)
        {
            try
            {
                await _travelPlanService.Update(request);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE api/<TravelPlanController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try {
                await _travelPlanService.Delete(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
