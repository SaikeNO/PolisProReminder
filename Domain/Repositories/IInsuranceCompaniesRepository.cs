using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsuranceCompaniesRepository
{
    Task<Guid> Create(InsuranceCompany entity);
    Task Delete(InsuranceCompany entity);
    Task<IEnumerable<InsuranceCompany>> GetAll(Guid agentId);
    Task<InsuranceCompany?> GetById(Guid agentId, Guid id);
    Task SaveChanges();
}
