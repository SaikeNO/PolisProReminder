using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsuranceTypeRepository(InsuranceDbContext dbContext) : IInsuranceTypeRepository
{
    public async Task<IEnumerable<InsuranceType>> GetAll()
    {
        var types = await dbContext
            .InsuranceTypes
            .ToListAsync();

        return types;
    }

    public async Task<InsuranceType?> GetById(Guid id)
    {
        var type = await dbContext
            .InsuranceTypes
            .FirstOrDefaultAsync(i => i.Id == id);

        return type;
    }

    public async Task Delete(InsuranceType entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(InsuranceType entity)
    {
        await dbContext.InsuranceTypes.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
