using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolisProReminder.API.Requests;
using PolisProReminder.Application.Attachments.Dtos;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Commands.CreatePolicy;
using PolisProReminder.Application.Policies.Commands.DeletePolicy;
using PolisProReminder.Application.Policies.Commands.PaidPolicies;
using PolisProReminder.Application.Policies.Commands.UpdatePolicyCommand;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Application.Policies.Queries.GetAllAttachments;
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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] string jsonString, [FromForm] IEnumerable<IFormFile> attachments)
    {
        CreatePolicyReq req = JsonConvert.DeserializeObject<CreatePolicyReq>(jsonString) ?? throw new BadHttpRequestException("Bad json");

        var command = new UpdatePolicyCommand()
        {
            Id = id,
            EndDate = DateOnly.FromDateTime(DateTime.Parse(req.EndDate)),
            StartDate = DateOnly.FromDateTime(DateTime.Parse(req.StartDate)),
            PaymentDate = req.PaymentDate is not null ? DateOnly.FromDateTime(DateTime.Parse(req.PaymentDate)) : null,
            InsuranceCompanyId = new Guid(req.InsuranceCompanyId),
            InsuranceTypeIds = req.InsuranceTypeIds.Select(x => new Guid(x)).ToList(),
            InsurerId = new Guid(req.InsurerId),
            IsPaid = req.IsPaid,
            PolicyNumber = req.PolicyNumber,
            Title = req.Title,
            Attachments = attachments
        };

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

    [HttpPatch("Paid")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PaidPolicies([FromBody] PaidPoliciesCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] string jsonString, [FromForm] IEnumerable<IFormFile> attachments)
    {
        CreatePolicyReq req = JsonConvert.DeserializeObject<CreatePolicyReq>(jsonString) ?? throw new BadHttpRequestException("Bad json");

        var command = new CreatePolicyCommand()
        {
            EndDate = DateOnly.FromDateTime(DateTime.Parse(req.EndDate)),
            StartDate = DateOnly.FromDateTime(DateTime.Parse(req.StartDate)),
            PaymentDate = req.PaymentDate is not null ? DateOnly.FromDateTime(DateTime.Parse(req.PaymentDate)) : null,
            InsuranceCompanyId = new Guid(req.InsuranceCompanyId),
            InsuranceTypeIds = req.InsuranceTypeIds.Select(x => new Guid(x)).ToList(),
            InsurerId = new Guid(req.InsurerId),
            IsPaid = req.IsPaid,
            PolicyNumber = req.PolicyNumber,
            Title = req.Title,
            Attachments = attachments
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

    [HttpGet("{id}/attachments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetAttachments([FromRoute] Guid id)
    {
        var attachmantes = await mediator.Send(new GetAllAttachmentsQuery(id));
        return Ok(attachmantes);
    }
}
