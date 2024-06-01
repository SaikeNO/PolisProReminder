using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Application.Policies.Queries.GetAllPolicies;

namespace PolisProReminder.Controllers
{
    [Route("api/[controller]")]
    public class PolicyController(IPoliciesService policyService, IMediator mediator) : ControllerBase
    {
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreatePolicyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await policyService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePolicy([FromRoute] Guid id)
        {
            await policyService.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateIsPaid([FromRoute] Guid id, [FromBody] bool isPaid)
        {
            await policyService.UpdateIsPaid(id, isPaid);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePolicyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await policyService.Create(dto);

            return Created($"api/policy/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<PolicyDto>>> GetAll([FromQuery] GetAllPoliciesQuery query)
        {
            var policies = await mediator.Send(query);
            return Ok(policies);
        }

        [HttpGet("Latest")]
        public async Task<ActionResult<IEnumerable<PolicyDto>>> GetLatest([FromQuery] int count)
        {
            var policies = await policyService.GetLatestPolicies(count);
            return Ok(policies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PolicyDto>> Get([FromRoute] Guid id)
        {
            var policy = await policyService.GetById(id);
            return Ok(policy);
        }
    }
}
