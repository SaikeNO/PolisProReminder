using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IPoliciesRepository
{
    Task<Guid> Create(Policy entity);
    Task Delete(Policy entity);
    Task<IEnumerable<Policy>> GetAll();
    Task<Policy?> GetById(Guid id);
    Task<Policy?> GetByNumber(string policyNumber);
    Task<IEnumerable<Policy>> GetLatestPolicies(int count);
    Task<(IEnumerable<Policy>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, Guid? typeId);
    Task SaveChanges();
}
