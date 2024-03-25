using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/[controller]")]
    public class PolicyController(IPolicyService policyService) : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy([FromRoute] int id, [FromBody] CreatePolicyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var policy = await policyService.UpdatePolicy(id, dto);

            return Ok(policy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy([FromRoute] int id)
        {
            await policyService.DeletePolicy(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateIsPaid([FromRoute] int id, [FromBody] bool isPaid)
        {
            var policy = await policyService.UpdateIsPaidPolicy(id, isPaid);
            return Ok(policy);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await policyService.CreatePolicy(dto);

            return Created($"api/policy/{id}", null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var policies = await policyService.GetAll();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var policy = await policyService.GetById(id);
            return Ok(policy);
        }
    }
}
