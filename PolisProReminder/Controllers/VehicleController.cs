using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolisProReminder.API.Requests;
using PolisProReminder.Application.Attachments.Dtos;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Vehicles.Commands.CreateVehicle;
using PolisProReminder.Application.Vehicles.Commands.DeleteVehicle;
using PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Application.Vehicles.Queries.GetAllAttachments;
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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] string jsonString, IEnumerable<IFormFile> attachments)
    {
        CreateVehicleReq req = JsonConvert.DeserializeObject<CreateVehicleReq>(jsonString) ?? throw new BadHttpRequestException("Bad json");
        var command = new UpdateVehicleCommand()
        {
            Id = id,
            Capacity = req.Capacity,
            FirstRegistrationDate = req.FirstRegistrationDate != null ? DateOnly.FromDateTime(DateTime.Parse(req.FirstRegistrationDate)) : null,
            ProductionYear = req.ProductionYear != null ? DateOnly.FromDateTime(DateTime.Parse(req.ProductionYear)) : null,
            KM = req.KM,
            KW = req.KW,
            Mileage = req.Mileage,
            Name = req.Name,
            RegistrationNumber = req.RegistrationNumber,
            VehicleBrandId = new Guid(req.VehicleBrandId),
            InsurerId = new Guid(req.InsurerId),
            VIN = req.VIN,
            Attachments = attachments,
        };

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
    public async Task<IActionResult> Create([FromForm] string jsonString, IEnumerable<IFormFile> attachments)
    {
        CreateVehicleReq req = JsonConvert.DeserializeObject<CreateVehicleReq>(jsonString) ?? throw new BadHttpRequestException("Bad json");

        var command = new CreateVehicleCommand()
        {
            Capacity = req.Capacity,
            FirstRegistrationDate = req.FirstRegistrationDate != null ? DateOnly.FromDateTime(DateTime.Parse(req.FirstRegistrationDate)) : null,
            ProductionYear = req.ProductionYear != null ? DateOnly.FromDateTime(DateTime.Parse(req.ProductionYear)) : null,
            KM = req.KM,
            KW = req.KW,
            Mileage = req.Mileage,
            Name = req.Name,
            RegistrationNumber = req.RegistrationNumber,
            VehicleBrandId = new Guid(req.VehicleBrandId),
            InsurerId = new Guid(req.InsurerId),
            VIN = req.VIN,
            Attachments = attachments,
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

    [HttpGet("{id}/attachments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetAttachments([FromRoute] Guid id)
    {
        var attachmantes = await mediator.Send(new GetAllAttachmentsQuery(id));
        return Ok(attachmantes);
    }
}
