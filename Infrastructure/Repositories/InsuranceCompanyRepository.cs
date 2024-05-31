using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsuranceCompanyRepository(InsuranceDbContext dbContext) : IInsuranceCompanyRepository
{
    public async Task<IEnumerable<InsuranceCompany>> GetAll()
    {
        var companies = await dbContext
            .InsuranceCompanies
            .ToListAsync();

        return companies;
    }

    public async Task<InsuranceCompany?> GetById(Guid id)
    {
        var company = await dbContext
            .InsuranceCompanies
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
