using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

public class InsuranceType : ICreatedBy
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CreatedByUserId { get; set; }
    public Guid CreatedByAgentId { get; set; }
    public IEnumerable<Policy> Policies { get; set; } = null!;
}
