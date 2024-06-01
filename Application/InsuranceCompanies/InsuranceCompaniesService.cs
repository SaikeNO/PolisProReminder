using AutoMapper;
using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.InsuranceCompanies;

internal class InsuranceCompaniesService(IInsuranceCompaniesRepository insuranceCompanyRepository, IMapper mapper) : IInsuranceCompaniesService
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

    public async Task Update(Guid id, CreateInsuranceCompanyDto dto)
    {
        var company = await insuranceCompanyRepository.GetById(id);

        _ = company ?? throw new NotFoundException("Towarzystwo ubezpieczeniowe o podanym ID nie istnieje");

        company.Name = dto.Name;
        company.ShortName = dto.ShortName;

        await insuranceCompanyRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        var company = await insuranceCompanyRepository.GetById(id);

        _ = company ?? throw new NotFoundException("Towarzystwo ubezpieczeniowe o podanym ID nie istnieje");

        await insuranceCompanyRepository.Delete(company);
        await insuranceCompanyRepository.SaveChanges();
    }

    public async Task<Guid> Create(CreateInsuranceCompanyDto dto)
    {
        var company = mapper.Map<InsuranceCompany>(dto);

        var companyId = await insuranceCompanyRepository.Create(company);
        await insuranceCompanyRepository.SaveChanges();

        return companyId;
    }
}
