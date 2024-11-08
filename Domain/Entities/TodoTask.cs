namespace PolisProReminder.Domain.Entities;

public class TodoTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public int Order { get; set; } = 1;
    public Guid CreatedByUserId { get; set; }
}
