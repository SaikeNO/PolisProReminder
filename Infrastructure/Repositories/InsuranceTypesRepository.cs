using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsuranceTypesRepository(InsuranceDbContext dbContext) : IInsuranceTypesRepository
{
    
    public async Task<IEnumerable<InsuranceType>> GetAll(Guid agentId)
    {
        var types = await dbContext
            .InsuranceTypes
            .CreatedByAgent(agentId)
            .ToListAsync();

        return types;
    }

    public async Task<IEnumerable<InsuranceType>> GetManyByIds(Guid agentId, IEnumerable<Guid> ids)
    {
        var types = await dbContext.InsuranceTypes
            .CreatedByAgent(agentId)
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();

        return types;
    }

    public async Task<InsuranceType?> GetById(Guid agentId, Guid id)
    {
        var type = await dbContext
            .InsuranceTypes
            .CreatedByAgent(agentId)
            .FirstOrDefaultAsync(i => i.Id == id);

        return type;
    }

    public async Task Delete(InsuranceType entity)
    {
        dbContext.Remove(entity);

        await SaveChanges();
    }

    public async Task<Guid> Create(InsuranceType entity)
    {
        await dbContext.InsuranceTypes.AddAsync(entity);
        await SaveChanges();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
