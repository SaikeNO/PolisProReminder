using PolisProReminder.Application.VehicleBrands.Dtos;

namespace PolisProReminder.Application.Vehicles.Dtos;

public record VehicleDto(Guid Id, string Name, string RegistrationNumber, DateOnly? FirstRegistrationDate, DateOnly? ProductionYear, string? VIN, int? KW, int? KM, int? Capacity, uint? Mileage, string Insurer, VehicleBrandDto VehicleBrand)
{
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    public string RegistrationNumber { get; set; } = RegistrationNumber;
    public DateOnly? FirstRegistrationDate { get; set; } = FirstRegistrationDate;
    public DateOnly? ProductionYear { get; set; } = ProductionYear;
    public string? VIN { get; set; } = VIN;
    public int? KW { get; set; } = KW;
    public int? KM { get; set; } = KM;
    public int? Capacity { get; set; } = Capacity;
    public uint? Mileage { get; set; } = Mileage;
    public string Insurer { get; set; } = Insurer;
    public VehicleBrandDto VehicleBrand { get; set; } = VehicleBrand;
}
