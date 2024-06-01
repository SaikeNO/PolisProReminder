﻿using AutoMapper;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies;

internal class PoliciesService(IPoliciesRepository policyRepository, IInsuranceTypesRepository insuranceTypeRepository, IMapper mapper) : IPoliciesService
{
    public async Task UpdateIsPaid(Guid id, bool isPaid)
    {
        var policy = await policyRepository.GetById(id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        policy.IsPaid = isPaid;
        await policyRepository.SaveChanges();
    }

    public async Task<IEnumerable<PolicyDto>> GetLatestPolicies(int count)
    {
        var policies = await policyRepository.GetLatestPolicies(count);

        return mapper.Map<IEnumerable<PolicyDto>>(policies);
    }
}
