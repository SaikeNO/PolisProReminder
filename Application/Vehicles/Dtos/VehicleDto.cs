using PolisProReminder.Application.VehicleBrands.Dtos;

namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehicleDto
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
    public VehicleBrandDto VehicleBrand { get; set; } = null!;

    public virtual VehicleInsurerDto Insurer { get; set; } = null!;
}
