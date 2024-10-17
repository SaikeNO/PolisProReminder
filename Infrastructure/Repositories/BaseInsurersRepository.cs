using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class BaseInsurersRepository(InsuranceDbContext dbContext) : IBaseInsurersRepository
{
    public async Task<IEnumerable<BaseInsurer>> GetAll(Guid agentId)
    {
        var insurers = await dbContext.Set<BaseInsurer>()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies.Where(p => p.IsDeleted == false))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .ToListAsync();

        return insurers;
    }

    public async Task<BaseInsurer?> GetById(Guid agentId, Guid id)
    {
        var insurer = await dbContext.Set<BaseInsurer>()
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(i => i.Id == id);

        return insurer;
    }

    public async Task<IEnumerable<BaseInsurer>> GetManyByIds(Guid agentId, IEnumerable<Guid> ids)
    {
        var insurers = await dbContext.Set<BaseInsurer>()
            .CreatedByAgent(agentId)
            .Where(i => ids.Contains(i.Id))
            .ToListAsync();

        return insurers;
    }

    public async Task<Guid> Create(BaseInsurer entity)
    {
        await dbContext.AddAsync(entity);
        await SaveChanges();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
