namespace PolisProReminder.Domain.Entities;

public class InsuranceCompany
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = string.Empty;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;
    public virtual List<Policy> Policies { get; set; } = [];
}
