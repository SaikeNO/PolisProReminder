using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsuranceCompaniesRepository(InsuranceDbContext dbContext) : IInsuranceCompaniesRepository
{
    public async Task<IEnumerable<InsuranceCompany>> GetAll(string agentId)
    {
        var companies = await dbContext
            .InsuranceCompanies
            .Where(c => c.CreatedByAgentId == agentId)
            .ToListAsync();

        return companies;
    }

    public async Task<InsuranceCompany?> GetById(string agentId, Guid id)
    {
        var company = await dbContext
            .InsuranceCompanies
            .Where(c => c.CreatedByAgentId == agentId)
            .FirstOrDefaultAsync(x => x.Id == id);

        return company;
    }

    public async Task Delete(InsuranceCompany entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(InsuranceCompany entity)
    {
        await dbContext
            .InsuranceCompanies
            .AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
