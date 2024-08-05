using System.ComponentModel.DataAnnotations.Schema;

namespace PolisProReminder.Domain.Entities;

public class Vehicle : AttachmentList
{
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public DateOnly? FirstRegistrationDate { get; set; }
    public DateOnly? ProductionYear { get; set; }
    public string? VIN {  get; set; }
    public int? KW { get; set; }
    public int? KM { get; set; }
    public int? Capacity { get; set; }
    public uint? Mileage { get; set; } 
    public bool IsDeleted { get; set; } = false;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;

    public virtual List<Policy> Policies { get; set; } = [];
    public virtual Insurer Insurer { get; set; } = null!;
    public virtual VehicleBrand VehicleBrand { get; set; } = null!;
}
