using PolisProReminder.Application.VehicleBrands.Dtos;

namespace PolisProReminder.Application.Vehicles.Dtos;

public record VehicleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
    public DateOnly? FirstRegistrationDate { get; set; }
    public DateOnly? ProductionYear { get; set; }
    public string? VIN { get; set; }
    public int? KW { get; set; }
    public int? KM { get; set; }
    public int? Capacity { get; set; }
    public uint? Mileage { get; set; }
    public string InsurerName { get; set; } = string.Empty;
    public Guid InsurerId { get; set; }
    public VehicleBrandDto VehicleBrand { get; set; } = null!;
}
