namespace PolisProReminder.Application.Attachments.Dtos;

public class AttachmentDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = null!;
    public string UniqueFileName { get; set; } = null!;
}
