using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Domain.Repositories;

public interface IInsuranceCompanyRepository
{
    Task<Guid> Create(InsuranceCompany entity);
    Task Delete(InsuranceCompany entity);
    Task<IEnumerable<InsuranceCompany>> GetAll();
    Task<InsuranceCompany?> GetById(Guid id);
    Task SaveChanges();
}
