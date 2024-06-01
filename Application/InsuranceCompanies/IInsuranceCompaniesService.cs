using PolisProReminder.Application.InsuranceCompanies.Dtos;

namespace PolisProReminder.Application.InsuranceCompanies
{
    public interface IInsuranceCompaniesService
    {
        Task<Guid> Create(CreateInsuranceCompanyDto dto);
        Task Delete(Guid id);
        Task<IEnumerable<InsuranceCompanyDto>> GetAll();
        Task<InsuranceCompanyDto?> GetById(Guid id);
        Task Update(Guid id, CreateInsuranceCompanyDto dto);
    }
}