using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers;

[Route("api/[controller]")]
public class InsuranceTypeController(IInsuranceTypeService insuranceTypeService) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CreateInsuranceTypeDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await insuranceTypeService.Update(id, dto);

        return Ok();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await insuranceTypeService.DeleteInsuranceType(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var type = await insuranceTypeService.GetById(id);

        return Ok(type);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInsuranceType([FromBody] CreateInsuranceTypeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await insuranceTypeService.CreateInsuranceType(dto);

        return Created($"/api/type/{id}", null);
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var types = await insuranceTypeService.GetAll();

        return Ok(types);
    }
}
