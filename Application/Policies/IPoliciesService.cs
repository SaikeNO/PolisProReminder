﻿using PolisProReminder.Application.Policies.Dtos;

namespace PolisProReminder.Application.Policies
{
    internal interface IPoliciesService
    {
        Task<Guid> CreatePolicy(CreatePolicyDto dto);
        Task Delete(Guid id);
        Task<PolicyDto> GetById(Guid id);
        Task<IEnumerable<PolicyDto>> GetLatestPolicies(int count);
        Task Update(Guid id, CreatePolicyDto dto);
        Task UpdateIsPaid(Guid id, bool isPaid);
    }
}