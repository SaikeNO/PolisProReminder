using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class AttachmentsRepository(InsuranceDbContext dbContext) : IAttachmentsRepository
{
    public async Task<IEnumerable<Attachment>?> GetAll<TEntity>(Guid id) where TEntity : AttachmentList
    {
        var set = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (set == null)
            return null;

        return set.Attachments;
    }

    public async Task<Attachment?> GetById(string agentId, Guid id)
    {
        var company = await dbContext
            .Attachments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return company;
    }

    public async Task Delete(Attachment entity)
    {
        entity.IsDeleted = true;

        await dbContext.SaveChangesAsync();
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
