using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsuranceTypesRepository
{
    Task<IEnumerable<InsuranceType>> GetAll(Guid agentId);
    Task<InsuranceType?> GetById(Guid agentId, Guid id);
    Task<IEnumerable<InsuranceType>> GetManyByIds(Guid agentId, IEnumerable<Guid> ids);
    Task Delete(InsuranceType entity);
    Task<Guid> Create(InsuranceType entity);
    Task SaveChanges();
}
