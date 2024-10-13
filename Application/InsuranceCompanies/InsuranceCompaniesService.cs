using AutoMapper;
using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.InsuranceCompanies;

internal class InsuranceCompaniesService(IUserContext userContext,
    IInsuranceCompaniesRepository insuranceCompanyRepository, 
    IMapper mapper) : IInsuranceCompaniesService
{
    private readonly CurrentUser CurrentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

    public async Task<IEnumerable<InsuranceCompanyDto>> GetAll()
    {
        var companies = await insuranceCompanyRepository.GetAll(CurrentUser.AgentId);

        return mapper.Map<IEnumerable<InsuranceCompanyDto>>(companies);
    }

    public async Task<InsuranceCompanyDto?> GetById(Guid id)
    {
        var company = await insuranceCompanyRepository.GetById(CurrentUser.AgentId, id);

        return mapper.Map<InsuranceCompanyDto?>(company);
    }

    public async Task Update(Guid id, CreateInsuranceCompanyDto dto)
    {
        var company = await insuranceCompanyRepository.GetById(CurrentUser.AgentId, id);

        _ = company ?? throw new NotFoundException("Towarzystwo ubezpieczeniowe o podanym ID nie istnieje");

        company.Name = dto.Name;
        company.ShortName = dto.ShortName;

        await insuranceCompanyRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        var company = await insuranceCompanyRepository.GetById(CurrentUser.AgentId, id);

        _ = company ?? throw new NotFoundException("Towarzystwo ubezpieczeniowe o podanym ID nie istnieje");

        await insuranceCompanyRepository.Delete(company);
        await insuranceCompanyRepository.SaveChanges();
    }

    public async Task<Guid> Create(CreateInsuranceCompanyDto dto)
    {
        var company = mapper.Map<InsuranceCompany>(dto);
        company.CreatedByAgentId = CurrentUser.AgentId;
        company.CreatedByUserId = CurrentUser.Id;

        var companyId = await insuranceCompanyRepository.Create(company);

        return companyId;
    }
}
