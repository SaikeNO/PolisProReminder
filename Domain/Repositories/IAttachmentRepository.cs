using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IAttachmentsRepository
{
    Task Delete(Attachment entity);
    Task<IEnumerable<Attachment>?> GetAll<TEntity>(Guid id) where TEntity : AttachmentList;
    Task<Attachment?> GetById(string agentId, Guid id);
    Task SaveChanges();
}
