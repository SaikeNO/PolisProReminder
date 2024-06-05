using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsuranceTypesRepository
{
    Task<IEnumerable<InsuranceType>> GetAll(string agentId);
    Task<InsuranceType?> GetById(string agentId, Guid id);
    Task<IEnumerable<InsuranceType>> GetManyByIds(string agentId, IEnumerable<Guid> ids);
    Task Delete(InsuranceType entity);
    Task<Guid> Create(InsuranceType entity);
    Task SaveChanges();
}
