using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Commands.CreateBusinessInsurer;
using PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;
using PolisProReminder.Application.Insurers.Commands.DeleteInsurer;
using PolisProReminder.Application.Insurers.Commands.UpdateBusinessInsurer;
using PolisProReminder.Application.Insurers.Commands.UpdateIndividualInsurer;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllBusinessInsurers;
using PolisProReminder.Application.Insurers.Queries.GetAllIndividualInsurers;
using PolisProReminder.Application.Insurers.Queries.GetAllInsurers;
using PolisProReminder.Application.Insurers.Queries.GetInsurerById;
using PolisProReminder.Application.Insurers.Queries.GetPaginatedBusinessInsurers;
using PolisProReminder.Application.Insurers.Queries.GetPaginatedIndividualInsurers;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class InsurerController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteInsurerCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseInsurerDto?>> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var insurer = await mediator.Send(new GetInsurerByIdQuery(id), cancellationToken);

        return Ok(insurer);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsurerBasicInfoDto>>> GetAll(CancellationToken cancellationToken)
    {
        var insurers = await mediator.Send(new GetAllInsurersQuery(), cancellationToken);

        return Ok(insurers);
    }

    [HttpPut("Individual/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateIndividual([FromRoute] Guid id, [FromBody] UpdateIndividualInsurerCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("Individual")]
    public async Task<IActionResult> CreateIndividual([FromBody] CreateIndividualInsurerCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("Individual")]
    public async Task<IActionResult> GetAllIndividual(CancellationToken cancellationToken)
    {
        var insurers = await mediator.Send(new GetAllIndividualInsurersQuery(), cancellationToken);

        return Ok(insurers);
    }

    [HttpGet("Individual/Paginated")]
    public async Task<ActionResult<PageResult<IndividualInsurerDto>>> GetIndividualPaginated([FromQuery] GetPaginatedIndividualInsurersQuery query, CancellationToken cancellationToken)
    {
        var insurers = await mediator.Send(query, cancellationToken);
        return Ok(insurers);
    }

    [HttpPut("Business/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBusiness([FromRoute] Guid id, [FromBody] UpdateBusinessInsurerCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("Business")]
    public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessInsurerCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("Business")]
    public async Task<ActionResult<IEnumerable<BusinessInsurerDto>>> GetAllBusiness([FromQuery] GetAllBusinessInsurersQuery query, CancellationToken cancellationToken)
    {
        var insurers = await mediator.Send(query, cancellationToken);
        return Ok(insurers);
    }

    [HttpGet("Business/Paginated")]
    public async Task<ActionResult<PageResult<BusinessInsurerDto>>> GetBusinessPaginated([FromQuery] GetPaginatedBusinessInsurersQuery query, CancellationToken cancellationToken)
    {
        var insurers = await mediator.Send(query, cancellationToken);
        return Ok(insurers);
    }
}
