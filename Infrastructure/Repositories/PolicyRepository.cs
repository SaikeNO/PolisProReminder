using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class PolicyRepository(InsuranceDbContext dbContext) : IPolicyRepository
{
    public async Task Delete(Policy entity)
    {
        entity.IsDeleted = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(Policy entity)
    {
        dbContext.Policies.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Policy>> GetAll()
    {
        var policies = await dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .ToListAsync();

        return policies;
    }

    public async Task<Policy?> GetById(Guid id)
    {
        var policy = await dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(p => p.Id == id);

        return policy;
    }

    public async Task<Policy?> GetByNumber(string policyNumber)
    {
        var policy = await dbContext
            .Policies
            .FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);

        return policy;
    }

    public async Task<IEnumerable<Policy>> GetLatestPolicies(int count)
    {
        var policies = await dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .OrderBy(p => p.EndDate)
            .Take(count)
            .ToListAsync();

        return policies;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

}
