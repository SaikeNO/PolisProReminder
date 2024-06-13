namespace PolisProReminder.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public DateOnly? FirstRegistrationDate { get; set; }
    public DateOnly? ProductionYear { get; set; }
    public string? VIN {  get; set; }
    public decimal? KW { get; set; }
    public decimal? KM { get; set; }
    public decimal? Capacity { get; set; }
    public uint? Mileage { get; set; } 
    public bool IsDeleted { get; set; } = false;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;

    public virtual List<Policy> Policies { get; set; } = [];
    public virtual Insurer Insurer { get; set; } = null!;
    public virtual VehicleBrand VehicleBrand { get; set; } = null!;
}
