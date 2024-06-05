using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IPoliciesRepository
{
    Task<Guid> Create(Policy entity);
    Task Delete(Policy entity);
    Task<IEnumerable<Policy>> GetAll(string agentId);
    Task<Policy?> GetById(string agentId, Guid id);
    Task<Policy?> GetByNumber(string agentId, string policyNumber);
    Task<IEnumerable<Policy>> GetLatestPolicies(string agentId, int count);
    Task<(IEnumerable<Policy>, int)> GetAllMatchingAsync(string agentId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, Guid? typeId);
    Task SaveChanges();
}
