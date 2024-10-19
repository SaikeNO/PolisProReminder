using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class PoliciesRepository(InsuranceDbContext dbContext) : IPoliciesRepository
{
    public void Delete(Policy entity)
    {
        entity.IsDeleted = true;
    }

    public async Task<Guid> Create(Policy entity)
    {
        dbContext.Policies.Add(entity);
        await SaveChanges();

        return entity.Id;
    }

    public async Task<IEnumerable<Policy>> GetAll(Guid agentId)
    {
        var policies = await dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurers)
            .Include(p => p.InsuranceTypes)
            .ToListAsync();

        return policies;
    }

    public async Task<IEnumerable<Policy>> GetByIds(Guid agentId, IEnumerable<Guid> ids)
    {
        var policies = await dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurers)
            .Include(p => p.InsuranceTypes)
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();

        return policies;
    }

    public async Task<Policy?> GetById(Guid agentId, Guid id)
    {
        var policy = await dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurers)
            .Include(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(p => p.Id == id);

        return policy;
    }

    public async Task<Policy?> GetByNumber(Guid agentId, string policyNumber)
    {
        var policy = await dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);

        return policy;
    }

    public async Task<Policy?> GetByNumber(Guid agentId, string policyNumber, string requestPolicyNumber)
    {
        var policy = await dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .FirstOrDefaultAsync(p => p.PolicyNumber != policyNumber && p.PolicyNumber == requestPolicyNumber);

        return policy;
    }


    public async Task<(IEnumerable<Policy>, int)> GetAllMatchingAsync(Guid agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection,
        Guid? typeId,
        bool isArchived)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurers)
            .Include(p => p.InsuranceTypes)
            .Where(p => searchPhraseLower == null
                || p.Title.ToLower().Contains(searchPhraseLower)
                || p.PolicyNumber.ToLower().Contains(searchPhraseLower)
                || dbContext.Set<IndividualInsurer>()
                    .FilterBySearchPhrase(searchPhraseLower)
                    .Any(i => p.Insurers.Select(insurer => insurer.Id).Contains(i.Id))
                || dbContext.Set<BusinessInsurer>()
                    .FilterBySearchPhrase(searchPhraseLower)
                    .Any(b => p.Insurers.Select(insurer => insurer.Id).Contains(b.Id)))
            .Where(p => typeId == null || p.InsuranceTypes.Any(t => t.Id == typeId))
            .Where(p => p.IsArchived == isArchived)
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

    public async Task<IEnumerable<Policy>> GetLatestPolicies(Guid agentId, int count)
    {
        var policies = await dbContext
            .Policies
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurers)
            .Include(p => p.InsuranceTypes)
            .OrderBy(p => p.EndDate)
            .Take(count)
            .ToListAsync();

        return policies;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

}
