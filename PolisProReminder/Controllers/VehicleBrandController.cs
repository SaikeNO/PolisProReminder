using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.VehicleBrands.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class VehicleBrandController(IVehicleBrandsRepository vehicleBrandsRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleBrandDto>>> GetAll()
    {
        var brands = await vehicleBrandsRepository.GetAll();

        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleBrandDto>> GetById([FromRoute] Guid id)
    {
        var brand = await vehicleBrandsRepository.GetById(id);
        return Ok(brand);
    }

}
