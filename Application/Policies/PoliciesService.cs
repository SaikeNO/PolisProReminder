using AutoMapper;
using Microsoft.Extensions.Logging;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies;

internal class PoliciesService(IPoliciesRepository policyRepository, IInsuranceTypesRepository insuranceTypeRepository, IMapper mapper, ILogger logger) : IPoliciesService
{
    public async Task Delete(Guid id)
    {
        var policy = await policyRepository.GetById(id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        await policyRepository.Delete(policy);
        await policyRepository.SaveChanges();
    }
    public async Task UpdateIsPaid(Guid id, bool isPaid)
    {
        var policy = await policyRepository.GetById(id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        policy.IsPaid = isPaid;
        await policyRepository.SaveChanges();
    }

    public async Task Update(Guid id, CreatePolicyDto dto)
    {
        var policy = await policyRepository.GetById(id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        if (await policyRepository.GetByNumber(dto.PolicyNumber) != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        List<InsuranceType> newTypes = [];
        foreach (var typeId in dto.InsuranceTypeIds)
        {
            var type = await insuranceTypeRepository.GetById(typeId);

            if (type is not null)
                newTypes.Add(type);
        }

        policy.PolicyNumber = dto.PolicyNumber;
        policy.InsuranceCompanyId = dto.InsuranceCompanyId;
        policy.StartDate = dto.StartDate;
        policy.EndDate = dto.EndDate;
        policy.PaymentDate = dto.PaymentDate;
        policy.InsurerId = dto.InsurerId;
        policy.IsPaid = dto.IsPaid;
        policy.Title = dto.Title;
        policy.InsuranceTypes.Clear();
        policy.InsuranceTypes.AddRange(newTypes);

        await policyRepository.SaveChanges();
    }

    public async Task<Guid> Create(CreatePolicyDto dto)
    {
        var policy = await policyRepository.GetByNumber(dto.PolicyNumber);

        if (policy != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        var types = await insuranceTypeRepository.GetManyByIds(dto.InsuranceTypeIds);

        var createPolicy = new Policy
        {
            PolicyNumber = dto.PolicyNumber,
            InsuranceCompanyId = dto.InsuranceCompanyId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            PaymentDate = dto.PaymentDate,
            InsurerId = dto.InsurerId,
            InsuranceTypes = types.ToList(),
            IsPaid = dto.IsPaid,
            Title = dto.Title,
        };

        await policyRepository.Create(createPolicy);

        return createPolicy.Id;
    }

    public async Task<PolicyDto> GetById(Guid id)
    {
        var policy = await policyRepository.GetById(id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        return mapper.Map<PolicyDto>(policy);
    }

    public async Task<IEnumerable<PolicyDto>> GetLatestPolicies(int count)
    {
        var policies = await policyRepository.GetLatestPolicies(count);

        return mapper.Map<IEnumerable<PolicyDto>>(policies);
    }
}
