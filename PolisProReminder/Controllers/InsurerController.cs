using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllInsurers;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers;

[Route("api/[controller]")]
public class InsurerController(IInsurerService insurerService, IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CreateInsurerDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await insurerService.Update(id, dto);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await insurerService.DeleteInsurer(id);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateInsurer([FromBody] CreateInsurerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await insurerService.CreateInsurer(dto);

        return Created($"/api/insurer/{id}", null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var insurers = await insurerService.GetAll();

        return Ok(insurers);
    }

    [HttpGet("getPaginated")]
    public async Task<IActionResult> GetPaginated([FromQuery] GetAllInsurersQuery query)
    {
        var insurers = await mediator.Send(query);
        return Ok(insurers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var insurer = await insurerService.GetById(id);

        return Ok(insurer);
    }
}
