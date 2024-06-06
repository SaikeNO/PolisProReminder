using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.API.Requests;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Commands.CreatePolicy;
using PolisProReminder.Application.Policies.Commands.DeletePolicy;
using PolisProReminder.Application.Policies.Commands.UpdatePolicyCommand;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Application.Policies.Queries.GetAllPolicies;
using PolisProReminder.Application.Policies.Queries.GetLatestPolicies;
using PolisProReminder.Application.Policies.Queries.GetPolicyById;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class PolicyController(IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePolicyCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePolicy([FromRoute] Guid id)
    {
        await mediator.Send(new DeletePolicyCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePolicyReq req)
    {
        var command = new CreatePolicyCommand()
        {
            EndDate = DateOnly.FromDateTime(DateTime.Parse(req.EndDate)),
            StartDate = DateOnly.FromDateTime(DateTime.Parse(req.StartDate)),
            PaymentDate = DateOnly.FromDateTime(DateTime.Parse(req.PaymentDate)),
            InsuranceCompanyId = new Guid(req.InsuranceCompanyId),
            InsuranceTypeIds = req.InsuranceTypeIds.Select(x => new Guid(x)).ToList(),
            InsurerId = new Guid(req.InsurerId),
            IsPaid = req.IsPaid,
            PolicyNumber = req.PolicyNumber,
            Title = req.Title,
        };

        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
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
        var policies = await mediator.Send(new GetLatestPoliciesQuery(count));
        return Ok(policies);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PolicyDto>> GetById([FromRoute] Guid id)
    {
        var policy = await mediator.Send(new GetPolicyByIdQuery(id));
        return Ok(policy);
    }
}
