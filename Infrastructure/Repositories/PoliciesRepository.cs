using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class PoliciesRepository(InsuranceDbContext dbContext) : IPoliciesRepository
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

    public async Task<(IEnumerable<Policy>, int)> GetAllMatchingAsync(string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection,
        Guid? typeId)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .Where(p => (searchPhraseLower == null || p.Title.ToLower().Contains(searchPhraseLower)
                                                    || p.PolicyNumber.ToLower().Contains(searchPhraseLower)
                                                    || p.Insurer.FirstName.ToLower().Contains(searchPhraseLower)
                                                    || p.Insurer.LastName.ToLower().Contains(searchPhraseLower))
                        && (typeId == null || p.InsuranceTypes.Any(t => t.Id == typeId))
                         && p.IsArchived == false
                         && p.IsDeleted == false)
            .OrderBy(p => p.EndDate);

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null && sortDirection != SortDirection.None)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Policy, object>>>
            {
                { nameof(PolicyDto.Title).ToLower(), p => p.Title },
                { nameof(PolicyDto.PolicyNumber).ToLower(), p => p.PolicyNumber },
                { nameof(PolicyDto.EndDate).ToLower(), p => p.EndDate },
                { nameof(PolicyDto.StartDate).ToLower(), p => p.StartDate },
                { nameof(PolicyDto.PaymentDate).ToLower(), p => p.PaymentDate }
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        var policies = await baseQuery
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync();

        return (policies, totalCount);
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
