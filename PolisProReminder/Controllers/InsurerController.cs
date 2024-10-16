﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;
using PolisProReminder.Application.Insurers.Commands.DeleteInsurer;
using PolisProReminder.Application.Insurers.Commands.UpdateIndividualInsurer;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllIndividualInsurers;
using PolisProReminder.Application.Insurers.Queries.GetIndividualInsurerById;
using PolisProReminder.Application.Insurers.Queries.GetPaginatedBusinessInsurers;
using PolisProReminder.Application.Insurers.Queries.GetPaginatedIndividualInsurers;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class InsurerController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteInsurerCommand(id));

        return NoContent();
    }

    [HttpPut("Individual/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateIndividual([FromRoute] Guid id, [FromBody] UpdateIndividualInsurerCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpPost("Individual")]
    public async Task<IActionResult> CreateIndividual([FromBody] CreateIndividualInsurerCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetIndividualById), new { id }, null);
    }

    [HttpGet("Individual")]
    public async Task<IActionResult> GetAllIndividual()
    {
        var insurers = await mediator.Send(new GetAllIndividualInsurersQuery());

        return Ok(insurers);
    }

    [HttpGet("Individual/Paginated")]
    public async Task<ActionResult<PageResult<IndividualInsurerDto>>> GetIndividualPaginated([FromQuery] GetPaginatedIndividualInsurersQuery query)
    {
        var insurers = await mediator.Send(query);
        return Ok(insurers);
    }


    [HttpGet("Individual/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IndividualInsurerDto?>> GetIndividualById([FromRoute] Guid id)
    {
        var insurer = await mediator.Send(new GetIndividualInsurerByIdQuery(id));

        return Ok(insurer);
    }

    [HttpGet("Business/Paginated")]
    public async Task<ActionResult<PageResult<BusinessInsurerDto>>> GetBusinessPaginated([FromQuery] GetPaginatedBusinessInsurersQuery query)
    {
        var insurers = await mediator.Send(query);
        return Ok(insurers);
    }
}
