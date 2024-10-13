using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsuranceCompaniesRepository(InsuranceDbContext dbContext) : IInsuranceCompaniesRepository
{
    public async Task<IEnumerable<InsuranceCompany>> GetAll(Guid agentId)
    {
        var companies = await dbContext
            .InsuranceCompanies
            .CreatedByAgent(agentId)
            .ToListAsync();

        return companies;
    }

    public async Task<InsuranceCompany?> GetById(Guid agentId, Guid id)
    {
        var company = await dbContext
            .InsuranceCompanies
            .CreatedByAgent(agentId)
            .FirstOrDefaultAsync(x => x.Id == id);

        return company;
    }

    public async Task Delete(InsuranceCompany entity)
    {
        dbContext.Remove(entity);

        await SaveChanges();
    }

    public async Task<Guid> Create(InsuranceCompany entity)
    {
        await dbContext
            .InsuranceCompanies
            .AddAsync(entity);

        await SaveChanges();

        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
