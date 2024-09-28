using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal abstract class BaseInsurersRepository<TEntity>(InsuranceDbContext dbContext) where TEntity : BaseInsurer
{
    public async Task<IEnumerable<TEntity>> GetAll(Guid agentId)
    {
        var insurers = await dbContext.Set<TEntity>()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies.Where(p => p.IsDeleted == false))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .ToListAsync();

        return insurers;
    }

    public async Task<TEntity?> GetById(Guid agentId, Guid id)
    {
        var insurer = await dbContext.Set<TEntity>()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(i => i.Id == id);

        return insurer;
    }

    public async Task<Guid> Create(TEntity entity)
    {
        await dbContext.AddAsync(entity);
        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
