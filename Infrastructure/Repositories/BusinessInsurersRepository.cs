using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class BusinessInsurersRepository : BaseInsurersRepository, IBusinessInsurersRepository
{
    private readonly InsuranceDbContext _dbContext;
    public BusinessInsurersRepository(InsuranceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(IEnumerable<BusinessInsurer>, int)> GetAllMatchingAsync(Guid agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = _dbContext
            .BusinessInsurers
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies.Where(p => p.IsDeleted == false))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .Where(i => searchPhraseLower == null || i.Name.ToLower().Contains(searchPhraseLower)
                                                    || i.Nip.ToLower().Contains(searchPhraseLower)
                                                    || i.Regon.ToLower().Contains(searchPhraseLower)
                                                    || i.Email.ToLower().Contains(searchPhraseLower)
                                                    || i.PhoneNumber.ToLower().Contains(searchPhraseLower));
        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null && sortDirection != SortDirection.None)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<BusinessInsurer, object>>>
            {
                { nameof(BusinessInsurerDto.Name).ToLower(), i => i.Name },
                { nameof(BusinessInsurerDto.Nip).ToLower(), i => i.Nip },
                { nameof(BusinessInsurerDto.Regon).ToLower(), i => i.Regon },
                { nameof(BusinessInsurerDto.Email).ToLower(), i => i.Email },
                { nameof(BusinessInsurerDto.PhoneNumber).ToLower(), i => i.PhoneNumber },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var insurers = await baseQuery
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync();

        return (insurers, totalCount);
    }

    public async Task<BusinessInsurer?> GetByNipRegonAndId(Guid agentId, string nip, string regon, Guid? id)
    {
        var insurer = await _dbContext
            .BusinessInsurers
            .CreatedByAgent(agentId)
            .NotDeleted()
            .FirstOrDefaultAsync(i => (i.Nip == nip || i.Regon == regon) && i.Id != id);
        return insurer;
    }
}
