using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

public class Vehicle : AttachmentList, ISoftDeletable, ICreatedBy
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
    public Guid CreatedByUserId { get; set; }
    public Guid CreatedByAgentId { get; set; }

    public virtual IEnumerable<Policy> Policies { get; set; } = null!;
    public virtual BaseInsurer Insurer { get; set; } = null!;
    public virtual VehicleBrand VehicleBrand { get; set; } = null!;
}
