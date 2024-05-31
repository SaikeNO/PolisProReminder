using PolisProReminder.Application.InsuranceCompanies.Dtos;

namespace PolisProReminder.Application.InsuranceCompanies
{
    internal interface IInsuranceCompaniesService
    {
        Task<Guid> Create(CreateInsuranceCompanyDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<InsuranceCompanyDto>> GetAll();
        Task<InsuranceCompanyDto?> GetById(Guid id);
        Task<bool> Update(Guid id, CreateInsuranceCompanyDto dto);
    }
}