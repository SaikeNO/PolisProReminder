using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers;

[Route("api/[controller]")]
public class InsurerController(IInsurerService insurerService) : ControllerBase
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


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var insurer = await insurerService.GetById(id);

        return Ok(insurer);
    }
}
