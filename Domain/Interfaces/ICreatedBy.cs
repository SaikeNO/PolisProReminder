namespace PolisProReminder.Domain.Interfaces;

public interface ICreatedBy
{
    Guid CreatedByUserId { get; set; }
    Guid CreatedByAgentId { get; set; }
}