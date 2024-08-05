using MediatR;
using Microsoft.AspNetCore.Http;

namespace PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public DateOnly? FirstRegistrationDate { get; set; }
    public DateOnly? ProductionYear { get; set; }
    public string? VIN { get; set; }
    public int? KW { get; set; }
    public int? KM { get; set; }
    public int? Capacity { get; set; }
    public uint? Mileage { get; set; }

    public Guid InsurerId { get; set; }
    public Guid VehicleBrandId { get; set; }

    public IEnumerable<IFormFile> Attachments { get; set; } = [];
}
