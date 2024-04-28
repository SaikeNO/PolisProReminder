using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Queries.GetAllPolicies;

public class GetAllPoliciesHandler(InsuranceDbContext dbContext, IMapper mapper, IUserContextService userContextService) : IRequestHandler<GetAllPoliciesQuery, IEnumerable<PolicyDto>>
{
    public async Task<IEnumerable<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
    {
        var searchPhraseLower = request.SearchPhrase?.ToLower();

        var policies = (await dbContext
            .Policies
            .Include(p => p.InsuranceCompany)
            .Include(p => p.Insurer)
            .Include(p => p.InsuranceTypes)
            .Where(p => searchPhraseLower == null || p.Title.ToLower().Contains(searchPhraseLower)
                                                    || p.PolicyNumber.ToLower().Contains(searchPhraseLower))
            .ToListAsync())
            .Where(GetPredicate)
            .OrderBy(p => p.EndDate)
            .ToList();

        return mapper.Map<List<PolicyDto>>(policies);
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
