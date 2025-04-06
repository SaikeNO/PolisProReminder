using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolisProReminder.API.Requests;
using PolisProReminder.Application.Attachments.Dtos;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Commands.CreatePolicy;
using PolisProReminder.Application.Policies.Commands.DeletePolicy;
using PolisProReminder.Application.Policies.Commands.DeletePolicyBatch;
using PolisProReminder.Application.Policies.Commands.PaidPolicies;
using PolisProReminder.Application.Policies.Commands.UpdatePolicy;
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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] string jsonString, [FromForm] IEnumerable<IFormFile> attachments, CancellationToken cancellationToken)
    {
        PolicyReq req = JsonConvert.DeserializeObject<PolicyReq>(jsonString) ?? throw new BadHttpRequestException("Bad json");

        var command = new UpdatePolicyCommand()
        {
            Id = id,
            EndDate = DateOnly.FromDateTime(DateTime.Parse(req.EndDate)),
            StartDate = DateOnly.FromDateTime(DateTime.Parse(req.StartDate)),
            PaymentDate = req.PaymentDate is not null ? DateOnly.FromDateTime(DateTime.Parse(req.PaymentDate)) : null,
            InsuranceCompanyId = new Guid(req.InsuranceCompanyId),
            InsuranceTypeIds = req.InsuranceTypeIds.Select(x => new Guid(x)).ToList(),
            InsurerIds = req.InsurerIds.Select(x => new Guid(x)).ToList(),
            IsPaid = req.IsPaid,
            PolicyNumber = req.PolicyNumber,
            Title = req.Title,
            Attachments = attachments,
            Note = req.Note,
        };

        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePolicy([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeletePolicyCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpDelete("Batch")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePoliciesBatch([FromBody] DeletePolicyBatchCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("Paid")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PaidPolicies([FromBody] PaidPoliciesCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] string jsonString, [FromForm] IEnumerable<IFormFile> attachments, CancellationToken cancellationToken)
    {
        PolicyReq req = JsonConvert.DeserializeObject<PolicyReq>(jsonString) ?? throw new BadHttpRequestException("Bad json");

        var command = new CreatePolicyCommand()
        {
            EndDate = DateOnly.FromDateTime(DateTime.Parse(req.EndDate)),
            StartDate = DateOnly.FromDateTime(DateTime.Parse(req.StartDate)),
            PaymentDate = req.PaymentDate is not null ? DateOnly.FromDateTime(DateTime.Parse(req.PaymentDate)) : null,
            InsuranceCompanyId = new Guid(req.InsuranceCompanyId),
            InsuranceTypeIds = req.InsuranceTypeIds.Select(x => new Guid(x)).ToList(),
            InsurerIds = req.InsurerIds.Select(x => new Guid(x)).ToList(),
            IsPaid = req.IsPaid,
            PolicyNumber = req.PolicyNumber,
            Title = req.Title,
            Attachments = attachments,
            Note = req.Note,
        };

        var id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<PolicyDto>>> GetAll([FromQuery] GetAllPoliciesQuery query, CancellationToken cancellationToken)
    {
        var policies = await mediator.Send(query, cancellationToken);
        return Ok(policies);
    }

    [HttpGet("Latest")]
    public async Task<ActionResult<IEnumerable<PolicyDto>>> GetLatest([FromQuery] int count, CancellationToken cancellationToken)
    {
        var policies = await mediator.Send(new GetLatestPoliciesQuery(count), cancellationToken);
        return Ok(policies);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PolicyDto>> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var policy = await mediator.Send(new GetPolicyByIdQuery(id), cancellationToken);
        return Ok(policy);
    }

    [HttpGet("{id}/attachments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetAttachments([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var attachmantes = await mediator.Send(new GetAllAttachmentsQuery(id), cancellationToken);
        return Ok(attachmantes);
    }
}
