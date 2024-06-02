using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace PolisProReminder.Infrastructure.Repositories;

internal class InsurersRepository(InsuranceDbContext dbContext) : IInsurersRepository
{
    public async Task<IEnumerable<Insurer>> GetAll(string agentId)
    {
        var insurers = await dbContext
            .Insurers
            .Where(i => i.CreatedByAgentId == agentId)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .ToListAsync();

        return insurers;
    }

    public async Task<Insurer?> GetById(string agentId, Guid id)
    {
        var insurer = await dbContext
            .Insurers
            .Where(i => i.CreatedByAgentId == agentId)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .FirstOrDefaultAsync(i => i.Id == id);

        return insurer;
    }

    public async Task<(IEnumerable<Insurer>, int)> GetAllMatchingAsync(string agentId,
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Insurers
            .Where(i => i.CreatedByAgentId == agentId)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceCompany)
            .Include(i => i.Policies)
            .ThenInclude(p => p.InsuranceTypes)
            .Where(i => searchPhraseLower == null || i.LastName.ToLower().Contains(searchPhraseLower)
                                                    || i.FirstName.ToLower().Contains(searchPhraseLower)
                                                    || i.Email.ToLower().Contains(searchPhraseLower)
                                                    || i.Pesel.ToLower().Contains(searchPhraseLower)
                                                    || i.PhoneNumber.ToLower().Contains(searchPhraseLower));
        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null && sortDirection != SortDirection.None)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Insurer, object>>>
            {
                { nameof(InsurerDto.FirstName).ToLower(), i => i.FirstName },
                { nameof(InsurerDto.LastName).ToLower(), i => i.LastName},
                { nameof(InsurerDto.Pesel).ToLower(), i => i.Pesel},
                { nameof(InsurerDto.Email).ToLower(), i => i.Email},
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var insurers = await baseQuery
            .Skip(pageSize * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (insurers, totalCount);
    }

    public async Task<Insurer?> GetByPeselAndId(string agentId, string pesel, Guid? id)
    {
        var insurer = await dbContext
            .Insurers
            .Where(i => i.CreatedByAgentId == agentId)
            .FirstOrDefaultAsync(i => i.Pesel == pesel && i.Id != id);
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
