using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

namespace PolisProReminder.Controllers;

[Route("api/[controller]")]
public class InsurerController(IInsurersService insurerService, IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateInsurerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await insurerService.Update(id, dto);

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await insurerService.Delete(id);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInsurerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await insurerService.Create(dto);

        return Created($"/api/insurer/{id}", null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var insurers = await insurerService.GetAll();

        return Ok(insurers);
    }

    [HttpGet("getPaginated")]
    public async Task<ActionResult<PageResult<InsurerDto>>> GetPaginated([FromQuery] GetAllInsurersQuery query)
    {
        var insurers = await mediator.Send(query);
        return Ok(insurers);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<InsurerDto?>> GetById([FromRoute] Guid id)
    {
        var insurer = await insurerService.GetById(id);

        return Ok(insurer);
    }
}
