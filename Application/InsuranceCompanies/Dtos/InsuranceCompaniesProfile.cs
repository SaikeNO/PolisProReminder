using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.InsuranceCompanies.Dtos;

public class InsuranceCompaniesProfile : Profile
{
    public InsuranceCompaniesProfile()
    {
        CreateMap<InsuranceCompany, InsuranceCompanyDto>()
            .ReverseMap();

        CreateMap<CreateInsuranceCompanyDto, InsuranceCompany>()
            .ReverseMap();
    }
}
