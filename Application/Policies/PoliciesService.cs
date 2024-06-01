using AutoMapper;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies;

internal class PoliciesService(IPoliciesRepository policyRepository, IInsuranceTypesRepository insuranceTypeRepository, IMapper mapper) : IPoliciesService
{
    public async Task<IEnumerable<PolicyDto>> GetLatestPolicies(int count)
    {
        var policies = await policyRepository.GetLatestPolicies(count);

        return mapper.Map<IEnumerable<PolicyDto>>(policies);
    }
}
