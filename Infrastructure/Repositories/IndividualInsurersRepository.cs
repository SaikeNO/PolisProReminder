using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class IndividualInsurersRepository : BaseInsurersRepository, IIndividualInsurersRepository
{
    private readonly InsuranceDbContext _dbContext;
    public IndividualInsurersRepository(InsuranceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BaseInsurer>> GetAllIndividual(Guid agentId)
    {
        var insurers = await _dbContext
            .IndividualInsurers
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies.Where(p => p.IsDeleted == false))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .ToListAsync();

        return insurers;
    }

    public async Task<(IEnumerable<IndividualInsurer>, int)> GetAllMatchingAsync(Guid agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = _dbContext
            .IndividualInsurers
            .CreatedByAgent(agentId)
            .NotDeleted()
            .Include(i => i.Policies.Where(p => p.IsDeleted == false))
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .FilterBySearchPhrase(searchPhraseLower);

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null && sortDirection != SortDirection.None)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<IndividualInsurer, object>>>
            {
                { nameof(IndividualInsurerDto.FirstName).ToLower(), i => i.FirstName },
                { nameof(IndividualInsurerDto.LastName).ToLower(), i => i.LastName },
                { nameof(IndividualInsurerDto.Pesel).ToLower(), i => i.Pesel },
                { nameof(IndividualInsurerDto.Email).ToLower(), i => i.Email },
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

    public async Task<IndividualInsurer?> GetByPeselAndId(Guid agentId, string pesel, Guid? id)
    {
        var insurer = await _dbContext
            .IndividualInsurers
            .CreatedByAgent(agentId)
            .NotDeleted()
            .FirstOrDefaultAsync(i => i.Pesel == pesel && i.Id != id);
        return insurer;
    }
}
