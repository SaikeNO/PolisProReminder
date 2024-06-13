namespace PolisProReminder.API.Requests;

public class CreateVehicleReq
{
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public string FirstRegistrationDate { get; set; }
    public string ProductionYear { get; set; }
    public string? VIN { get; set; }
    public decimal? KW { get; set; }
    public decimal? KM { get; set; }
    public decimal? Capacity { get; set; }
    public uint? Mileage { get; set; }

    public string InsurerId { get; set; }
    public string VehicleBrandId { get; set; }
}
