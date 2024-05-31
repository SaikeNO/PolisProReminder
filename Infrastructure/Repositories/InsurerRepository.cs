using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsurerRepository(InsuranceDbContext dbContext) : IInsurerRepository
{
    public async Task<IEnumerable<Insurer>> GetAll()
    {
        var insurers = await dbContext
            .Insurers
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .ToListAsync();

        return insurers;
    }

    public async Task<Insurer?> GetById(Guid id)
    {
        var insurer = await dbContext
            .Insurers
            .Include(i => i.Policies.OrderBy(p => p.EndDate))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(i => i.Id == id);

        return insurer;
    }

    public async Task Delete(Insurer entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(Insurer entity)
    {
        await dbContext.Insurers.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

}
