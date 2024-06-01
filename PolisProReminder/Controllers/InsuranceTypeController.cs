using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.InsuranceTypes;
using PolisProReminder.Application.InsuranceTypes.Dtos;

namespace PolisProReminder.Controllers;

[Route("api/[controller]")]
public class InsuranceTypeController(IInsuranceTypesService insuranceTypeService) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateInsuranceTypeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await insuranceTypeService.Update(id, dto);

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await insuranceTypeService.Delete(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateInsuranceType([FromBody] CreateInsuranceTypeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await insuranceTypeService.Create(dto);

        return Created($"/api/InsuranceType/{id}", null);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InsuranceTypeDto?>> GetById([FromRoute] Guid id)
    {
        var type = await insuranceTypeService.GetById(id);

        return Ok(type);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsuranceTypeDto>>> GetAll()
    {
        var types = await insuranceTypeService.GetAll();

        return Ok(types);
    }
}
