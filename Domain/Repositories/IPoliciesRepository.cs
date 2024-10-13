using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IPoliciesRepository
{
    Task<Guid> Create(Policy entity);
    void Delete(Policy entity);
    Task<IEnumerable<Policy>> GetAll(Guid agentId);
    Task<Policy?> GetById(Guid agentId, Guid id);
    Task<IEnumerable<Policy>> GetByIds(Guid agentId, IEnumerable<Guid> ids);
    Task<Policy?> GetByNumber(Guid agentId, string policyNumber);
    Task<Policy?> GetByNumber(Guid agentId, string policyNumber, string requestPolicyNumber);
    Task<IEnumerable<Policy>> GetLatestPolicies(Guid agentId, int count);
    Task<(IEnumerable<Policy>, int)> GetAllMatchingAsync(Guid agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, Guid? typeId, bool isArchived);
    Task SaveChanges();
}
