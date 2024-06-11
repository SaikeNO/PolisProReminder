using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public DateOnly? FirstRegistrationDate { get; set; }
    public string? VIN { get; set; }

    public VehicleInsurerDto Insurer { get; set; } = null!;
}
