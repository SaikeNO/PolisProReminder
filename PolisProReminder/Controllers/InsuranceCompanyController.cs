using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers;

[Route("api/[controller]")]
public class InsuranceCompanyController(IInsuranceCompanyService insuranceCompanyService) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CreateInsuranceCompanyDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await insuranceCompanyService.Update(id, dto);

        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInsuranceCompany([FromBody] CreateInsuranceCompanyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await insuranceCompanyService.Create(dto);

        return Ok(company);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await insuranceCompanyService.Delete(id);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await insuranceCompanyService.GetAll();

        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var company = await insuranceCompanyService.Get(id);
        return Ok(company);
    }
}
