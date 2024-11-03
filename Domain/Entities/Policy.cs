using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

public class Policy : ISoftDeletable, ICreatedBy
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public Guid? InsuranceCompanyId { get; set; }
    public Guid? VehicleId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public bool IsPaid { get; set; } = false;
    public string? Note { get; set; }
    public bool IsArchived { get; set; } = false;

    public bool IsDeleted { get; set; } = false;
    public Guid CreatedByUserId { get; set; }
    public Guid CreatedByAgentId { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
    public virtual InsuranceCompany? InsuranceCompany { get; set; }
    public virtual List<InsuranceType> InsuranceTypes { get; set; } = null!;
    public virtual List<BaseInsurer> Insurers { get; set; } = null!;
    public virtual List<Attachment> Attachments { get; set; } = null!;
}
