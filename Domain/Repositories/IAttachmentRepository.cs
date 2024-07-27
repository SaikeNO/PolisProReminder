using Microsoft.AspNetCore.Http;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IAttachmentsRepository
{
    Task Delete(Attachment entity);
    Task<IEnumerable<Attachment>?> GetAll<TEntity>(Guid id) where TEntity : AttachmentList;
    Task<Attachment?> GetById(Guid id);
    Task SaveChanges();
    Task<IEnumerable<Attachment>> UploadAttachmentsAsync(IEnumerable<IFormFile> files, string savePath);
    Task<(FileStream, string)?> GetAttachmentAsync(Guid attachmentId);
}
