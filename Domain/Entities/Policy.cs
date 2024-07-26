namespace PolisProReminder.Domain.Entities;

public class Policy : AttachmentList
{
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public Guid InsurerId { get; set; }
    public Guid? InsuranceCompanyId { get; set; }
    public Guid? VehicleId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly PaymentDate { get; set; }
    public bool IsPaid { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public bool IsArchived { get; set; } = false;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;

    public virtual Insurer Insurer { get; set; } = null!;
    public virtual Vehicle? Vehicle { get; set; }
    public virtual InsuranceCompany? InsuranceCompany { get; set; }
    public virtual List<InsuranceType> InsuranceTypes { get; set; } = [];
}
