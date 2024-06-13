using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.API.Requests;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Commands.CreatePolicy;
using PolisProReminder.Application.Vehicles.Commands.CreateVehicle;
using PolisProReminder.Application.Vehicles.Commands.DeleteVehicle;
using PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Application.Vehicles.Queries.GetAllVehicles;
using PolisProReminder.Application.Vehicles.Queries.GetVehicleById;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class VehicleController(IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateVehicleCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePolicy([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteVehicleCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleReq req)
    {
        var command = new CreateVehicleCommand()
        {
            Capacity = req.Capacity,
            FirstRegistrationDate = DateOnly.FromDateTime(DateTime.Parse(req.FirstRegistrationDate)),
            ProductionYear = DateOnly.FromDateTime(DateTime.Parse(req.ProductionYear)),
            KM = req.KM,
            KW = req.KW,
            Mileage = req.Mileage,
            Name = req.Name,
            RegistrationNumber = req.RegistrationNumber,
            VehicleBrandId = new Guid(req.VehicleBrandId),
            InsurerId = new Guid(req.InsurerId),
            VIN = req.VIN,
        };

        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<VehicleDto>>> GetAll([FromQuery] GetAllVehiclesQuery query)
    {
        var vehicles = await mediator.Send(query);
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleDto>> GetById([FromRoute] Guid id)
    {
        var vehicle = await mediator.Send(new GetVehicleByIdQuery(id));
        return Ok(vehicle);
    }
}
