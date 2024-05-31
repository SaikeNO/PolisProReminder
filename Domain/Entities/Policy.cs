namespace PolisProReminder.Domain.Entities;

public class Policy
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public Guid InsurerId { get; set; }
    public Guid? InsuranceCompanyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaid { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public bool IsArchived { get; set; } = false;
    public Guid CreatedById { get; set; }
    public virtual User CreatedBy { get; set; } = null!;

    public virtual Insurer Insurer { get; set; } = null!;
    public virtual InsuranceCompany? InsuranceCompany { get; set; }
    public virtual List<InsuranceType> InsuranceTypes { get; set; } = [];
}
