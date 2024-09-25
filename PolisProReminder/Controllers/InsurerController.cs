using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;
using PolisProReminder.Application.Insurers.Commands.DeleteInsurer;
using PolisProReminder.Application.Insurers.Commands.UpdateInsurer;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllInsurers;
using PolisProReminder.Application.Insurers.Queries.GetInsurerById;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class InsurerController(IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateInsurerCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteInsurerCommand(id));

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIndividualInsurerCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var insurers = await mediator.Send(new GetAllInsurersQuery());

        return Ok(insurers);
    }

    [HttpGet("getPaginated")]
    public async Task<ActionResult<PageResult<InsurerDto>>> GetPaginated([FromQuery] GetPaginatedInsurersQuery query)
    {
        var insurers = await mediator.Send(query);
        return Ok(insurers);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsurerDto?>> GetById([FromRoute] Guid id)
    {
        var insurer = await mediator.Send(new GetInsurerByIdQuery(id));

        return Ok(insurer);
    }
}
