using BrunoZell.ModelBinding;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using PolisProReminder.API.Requests;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Vehicles.Commands.CreateVehicle;
using PolisProReminder.Application.Vehicles.Commands.DeleteVehicle;
using PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Application.Vehicles.Queries.GetAllVehicles;
using PolisProReminder.Application.Vehicles.Queries.GetVehicleById;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class VehicleController(IMediator mediator, IConfiguration configuration) : ControllerBase
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateVehicleReq req)
    {
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
        CreateVehicleReq req = JsonConvert.DeserializeObject<CreateVehicleReq>(jsonString);

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

    [AllowAnonymous]
    [HttpPost("{id}/upload")]
    public async Task<IActionResult> UploadFile(string id, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var storagePath = configuration["StoragePath"];

        if (storagePath == null)
            throw new ArgumentNullException(nameof(storagePath));

        //"/app/uploads" this location is virtual, we will map this to azure file share in this video
        // Generate a unique filename with incremental number
        var uniqueFileName = $"{id}_{DateTime.Now.Ticks}_{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(storagePath, uniqueFileName);


        try
        {
            // Save the uploaded file to the specified path
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Image uploaded successfully.");
        }
        catch (Exception ex)
        {
            // Handle any error that occurred during file upload
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to upload image." + ex.ToString());
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}/file/{fileId}")]
    public IActionResult GetFile([FromRoute] string id, [FromRoute] string fileId)
    {
        try
        {
            var storagePath = configuration["StoragePath"];

            if (storagePath == null)
                throw new ArgumentNullException(nameof(storagePath));

            var filePath = Path.Combine(storagePath, fileId);

            if (System.IO.File.Exists(filePath))
            {
                return File(System.IO.File.OpenRead(filePath), "application/octet-stream", Path.GetFileName(filePath));
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            // Handle any error that occurred during file upload
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve uploaded images." + ex.ToString());
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}/files")]
    public IActionResult GetFiles([FromRoute] string id)
    {
        try
        {
            var storagePath = configuration["StoragePath"];

            if (storagePath == null)
                throw new ArgumentNullException(nameof(storagePath));

            var files = Directory.GetFiles(storagePath, $"{id}_*")
                                      .Select(Path.GetFileName);

            return Ok(files);
        }
        catch (Exception ex)
        {
            // Handle any error that occurred during file upload
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve uploaded images." + ex.ToString());
        }
    }
}
