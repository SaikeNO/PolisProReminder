namespace PolisProReminder.Application.Attachments.Dtos;

public class AttachmentDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public string UniqueFileName { get; set; } = null!;
    public DateTime CreatedTime { get; set; }
}
