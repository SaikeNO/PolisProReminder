﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;

namespace PolisProReminder.Services;

public interface IInsuranceCompanyService
{
    Task<InsuranceCompanyDto> Create(CreateInsuranceCompanyDto dto);
    Task Delete(int id);
    Task<InsuranceCompanyDto> Get(int id);
    Task<IEnumerable<InsuranceCompanyDto>> GetAll();
    Task<InsuranceCompanyDto> Update(int id, CreateInsuranceCompanyDto dto);
}

public class InsuranceCompanyService(InsuranceDbContext dbContext, IMapper mapper) : IInsuranceCompanyService
{
    public async Task<IEnumerable<InsuranceCompanyDto>> GetAll()
    {
        var companies = await dbContext
            .InsuranceCompanies
            .ToListAsync();

        return mapper.Map<List<InsuranceCompanyDto>>(companies);
    }

    public async Task<InsuranceCompanyDto> Get(int id)
    {
        var company = await dbContext
            .InsuranceCompanies
            .FirstOrDefaultAsync(x => x.Id == id);

        if (company == null)
            throw new NotFoundException("Insurance Company not found");

        return mapper.Map<InsuranceCompanyDto>(company);
    }

    public async Task<InsuranceCompanyDto> Update(int id, CreateInsuranceCompanyDto dto)
    {
        var company = await dbContext
            .InsuranceCompanies
            .FirstOrDefaultAsync(c => c.Id == id);

        if (company == null)
            throw new NotFoundException("Insurance Company not found");

        company.Name = dto.Name;
        company.ShortName = dto.ShortName;

        await dbContext.SaveChangesAsync();

        return mapper.Map<InsuranceCompanyDto>(company);
    }

    public async Task Delete(int id)
    {
        var company = await dbContext
            .InsuranceCompanies
            .FirstOrDefaultAsync(x => x.Id == id);

        if (company == null)
            throw new NotFoundException("Insurance Company not found");

        dbContext
            .InsuranceCompanies
            .Remove(company);

        await dbContext.SaveChangesAsync();
    }

    public async Task<InsuranceCompanyDto> Create(CreateInsuranceCompanyDto dto)
    {
        var company = await dbContext
            .InsuranceCompanies
            .FirstOrDefaultAsync(c => c.Name == dto.Name);

        if (company != null)
            throw new AlreadyExistsException($"{company.Name} already exists");

        var createCompany = mapper.Map<InsuranceCompany>(dto);

        var createdCompany = await dbContext
            .InsuranceCompanies
            .AddAsync(createCompany);

        await dbContext.SaveChangesAsync();

        return mapper.Map<InsuranceCompanyDto>(createdCompany.Entity);
    }

}
