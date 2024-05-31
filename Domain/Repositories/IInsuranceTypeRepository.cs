using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsuranceTypeRepository
{
    Task<IEnumerable<InsuranceType>> GetAll();
    Task<InsuranceType?> GetById(Guid id);
    Task<IEnumerable<InsuranceType>> GetManyByIds(IEnumerable<Guid> ids);
    Task Delete(InsuranceType entity);
    Task<Guid> Create(InsuranceType entity);
    Task SaveChanges();
}
