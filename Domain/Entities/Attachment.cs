using Microsoft.AspNetCore.Http;

namespace PolisProReminder.Domain.Entities;

public class Attachment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = null!;
    public string UniqueFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;

    public Attachment() { }

    public Attachment(string fileName, string savePath)
    {
        FileName = fileName;
        UniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Id}_{fileName}";
        FilePath = Path.Combine(savePath, UniqueFileName);
    }
}

public abstract class AttachmentList
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<Attachment> Attachments { get; set; } = [];
}

public class AttachmentFormFile(IFormFile file, string filePath)
{
    public IFormFile File { get; } = file;
    public string FilePath { get; } = filePath;
}
