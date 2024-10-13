namespace PolisProReminder.Domain.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}