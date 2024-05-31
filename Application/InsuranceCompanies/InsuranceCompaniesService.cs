using AutoMapper;
using Microsoft.Extensions.Logging;
using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.InsuranceCompanies;

internal class InsuranceCompaniesService(IInsuranceCompaniesRepository insuranceCompanyRepository, IMapper mapper, ILogger logger) : IInsuranceCompaniesService
{
    public async Task<IEnumerable<InsuranceCompanyDto>> GetAll()
    {
        var companies = await insuranceCompanyRepository.GetAll();

        return mapper.Map<List<InsuranceCompanyDto>>(companies);
    }

    public async Task<InsuranceCompanyDto?> GetById(Guid id)
    {
        var company = await insuranceCompanyRepository.GetById(id);

        return mapper.Map<InsuranceCompanyDto?>(company);
    }

    public async Task<bool> Update(Guid id, CreateInsuranceCompanyDto dto)
    {
        var company = await insuranceCompanyRepository.GetById(id);

        if (company == null)
            return false;

        company.Name = dto.Name;
        company.ShortName = dto.ShortName;

        await insuranceCompanyRepository.SaveChanges();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var company = await insuranceCompanyRepository.GetById(id);

        if (company == null)
            return false;

        await insuranceCompanyRepository.Delete(company);
        await insuranceCompanyRepository.SaveChanges();

        return true;
    }

    public async Task<Guid> Create(CreateInsuranceCompanyDto dto)
    {
        var company = mapper.Map<InsuranceCompany>(dto);

        var companyId = await insuranceCompanyRepository.Create(company);
        await insuranceCompanyRepository.SaveChanges();

        return companyId;
    }
}
