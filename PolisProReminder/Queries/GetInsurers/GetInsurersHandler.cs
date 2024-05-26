using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Common;
using PolisProReminder.Entities;
using PolisProReminder.Models;
using PolisProReminder.Services;
using System.Linq.Expressions;

namespace PolisProReminder.Queries.GetInsurers;

public class GetInsurersHandler(InsuranceDbContext dbContext, IMapper mapper, IUserContextService userContextService) : IRequestHandler<GetInsurersQuery, PageResult<InsurerDto>>
{
    public async Task<PageResult<InsurerDto>> Handle(GetInsurersQuery request, CancellationToken cancellationToken)
    {
        var searchPhraseLower = request.SearchPhrase?.ToLower();

        var baseQuery = dbContext
            .Insurers
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

        if (request.SortBy != null && request.SortDirection != SortDirection.None)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Insurer, object>>>
            {
                { nameof(InsurerDto.FirstName).ToLower(), i => i.FirstName },
                { nameof(InsurerDto.LastName).ToLower(), i => i.LastName},
                { nameof(InsurerDto.Pesel).ToLower(), i => i.Pesel},
                { nameof(InsurerDto.Email).ToLower(), i => i.Email},
            };

            var selectedColumn = columnsSelector[request.SortBy];

            baseQuery = request.SortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var insurers = (await baseQuery
            .Skip(request.PageSize * (request.PageIndex))
            .Take(request.PageSize)
            .ToListAsync());

        var insurersDtos = mapper.Map<List<InsurerDto>>(insurers);

        var result = new PageResult<InsurerDto>(insurersDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
