namespace PolisProReminder.Application.Vehicles.Commands.BaseVehicle;

public class BaseVehicleCommand
{
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public DateOnly? FirstRegistrationDate { get; set; }
    public DateOnly? ProductionYear { get; set; }
    public string? VIN { get; set; }
    public int? KW { get; set; }
    public int? KM { get; set; }
    public int? Capacity { get; set; }
    public uint? Mileage { get; set; }

    public Guid VehicleBrandId { get; set; }

    public IEnumerable<Guid> InsurerIds { get; set; } = [];
    public IEnumerable<Guid> AttachmentIds { get; set; } = [];
}
