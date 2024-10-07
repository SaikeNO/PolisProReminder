using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

public class InsuranceCompany : ICreatedBy
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
    public Guid CreatedByAgentId { get; set; }
    public virtual IEnumerable<Policy> Policies { get; set; }
}
