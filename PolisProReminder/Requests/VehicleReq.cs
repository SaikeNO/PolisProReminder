namespace PolisProReminder.API.Requests;

public class VehicleReq
{
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public string FirstRegistrationDate { get; set; }
    public string ProductionYear { get; set; }
    public string? VIN { get; set; }
    public int? KW { get; set; }
    public int? KM { get; set; }
    public int? Capacity { get; set; }
    public uint? Mileage { get; set; }

    public IEnumerable<string> InsurerIds { get; set; }
    public string VehicleBrandId { get; set; }
}
