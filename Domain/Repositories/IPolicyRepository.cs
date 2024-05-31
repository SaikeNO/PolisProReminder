using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IPolicyRepository
{
    Task<Guid> Create(Policy entity);
    Task Delete(Policy entity);
    Task<IEnumerable<Policy>> GetAll();
    Task<Policy?> GetById(Guid id);
    Task<Policy?> GetByNumber(string policyNumber);
    Task<IEnumerable<Policy>> GetLatestPolicies(int count);
    Task SaveChanges();
}
