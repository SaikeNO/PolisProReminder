﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Common;
using PolisProReminder.Entities;
using PolisProReminder.Models;
using PolisProReminder.Services;
using System.Linq.Expressions;

namespace PolisProReminder.Queries.GetAllPolicies;

public class GetAllPoliciesHandler(InsuranceDbContext dbContext, IMapper mapper, IUserContextService userContextService) : IRequestHandler<GetAllPoliciesQuery, PageResult<PolicyDto>>
{
    public async Task<PageResult<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
    {
        var searchPhraseLower = request.SearchPhrase?.ToLower();

        var baseQuery = dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .Where(p => searchPhraseLower == null || p.Title.ToLower().Contains(searchPhraseLower)
                                                    || p.PolicyNumber.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync();

        if (request.SortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Policy, object>>>
            {
                { nameof(PolicyDto.Title), p => p.Title },
                { nameof(PolicyDto.PolicyNumber), p => p.PolicyNumber },
                { nameof(PolicyDto.EndDate), p => p.EndDate },
                { nameof(PolicyDto.StartDate), p => p.StartDate },
                { nameof(PolicyDto.PaymentDate), p => p.PaymentDate }
            };

            var selectedColumn = columnsSelector[request.SortBy];

            baseQuery = request.SortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var policies = (await baseQuery
            .Skip(request.PageSize * (request.PageIndex - 1))
            .Take(request.PageSize)
            .ToListAsync())
            .Where(GetPredicate)
            .ToList();

        var policiesDtos = mapper.Map<List<PolicyDto>>(policies);

        var result = new PageResult<PolicyDto>(policiesDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }

    private bool GetPredicate(Policy p)
    {
        var user = userContextService.User;
        if (user is not null && user.IsInRole("Admin"))
            return true;

        if (p.CreatedById == userContextService.GetUserId)
            return true;

        var superiorId = userContextService.GetSuperiorId;

        if (superiorId is not null && p.CreatedById == superiorId)
            return true;

        return false;
    }
}
