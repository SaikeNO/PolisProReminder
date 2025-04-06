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
    public async Task<ActionResult<IEnumerable<VehicleBrandDto>>> GetAll(CancellationToken cancellationToken)
    {
        var brands = await vehicleBrandsRepository.GetAll(cancellationToken);

        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleBrandDto>> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var brand = await vehicleBrandsRepository.GetById(id, cancellationToken);
        return Ok(brand);
    }

}
