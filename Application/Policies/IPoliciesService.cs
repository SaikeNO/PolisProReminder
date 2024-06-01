using PolisProReminder.Application.Policies.Dtos;

namespace PolisProReminder.Application.Policies
{
    public interface IPoliciesService
    {
        Task<Guid> Create(CreatePolicyDto dto);
        Task Delete(Guid id);
        Task<PolicyDto> GetById(Guid id);
        Task<IEnumerable<PolicyDto>> GetLatestPolicies(int count);
        Task Update(Guid id, CreatePolicyDto dto);
        Task UpdateIsPaid(Guid id, bool isPaid);
    }
}