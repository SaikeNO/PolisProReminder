using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IAttachmentsRepository
{
    void Delete(Attachment entity);
    Task<IEnumerable<Attachment>?> GetAll<TEntity>(Guid id) where TEntity : AttachmentList;
    Task<Attachment?> GetById(Guid id);
    Task SaveChanges();
    Task<IEnumerable<Attachment>> CreateAttachmentRangeAsync(IEnumerable<Attachment> attachments);
    Task UploadAttachmentsAsync(IEnumerable<AttachmentFormFile> attachments);
    Task<(FileStream, string)?> GetAttachmentAsync(Guid attachmentId);
}
