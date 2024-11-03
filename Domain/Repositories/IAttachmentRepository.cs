using Microsoft.AspNetCore.Http;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IAttachmentsRepository
{
    void Delete(Attachment entity);
    Task<(FileStream, string)?> GetAttachmentAsync(Guid attachmentId);
    Task<IEnumerable<Attachment>> GetManyByIds(IEnumerable<Guid> ids);
    Task<Attachment?> GetById(Guid id);
    Task SaveAttachmentAsync(Attachment attachment);
    Task SaveChanges();
    Task UploadAndSaveAttachmentAsync(Attachment attachment, IFormFile file);
    Task UploadAttachmentAsync(IFormFile attachment, string savePath);
}
