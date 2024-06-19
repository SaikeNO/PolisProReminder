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
    public decimal? KW { get; set; }
    public decimal? KM { get; set; }
    public decimal? Capacity { get; set; }
    public uint? Mileage { get; set; }
    public string VehicleBrand { get; set; } = null!;

    public virtual VehicleInsurerDto Insurer { get; set; } = null!;
}
