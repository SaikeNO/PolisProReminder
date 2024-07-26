namespace PolisProReminder.Domain.Entities;

public class Attachment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = null!;
    public string UniqueFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}

public abstract class AttachmentList
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public IEnumerable<Attachment> Attachments { get; set; } = [];
}