using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.InsuranceCompanies;
using PolisProReminder.Application.InsuranceCompanies.Dtos;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class InsuranceCompanyController(IInsuranceCompaniesService insuranceCompanyService) : ControllerBase
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateInsuranceCompanyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await insuranceCompanyService.Update(id, dto);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInsuranceCompanyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await insuranceCompanyService.Create(dto);

        return Created($"/api/InsuranceCompany/{id}", null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await insuranceCompanyService.Delete(id);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsuranceCompanyDto>>> GetAll()
    {
        var companies = await insuranceCompanyService.GetAll();

        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InsuranceCompanyDto>> GetById([FromRoute] Guid id)
    {
        var company = await insuranceCompanyService.GetById(id);
        return Ok(company);
    }
}
