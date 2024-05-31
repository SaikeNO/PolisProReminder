using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Application.InsuranceTypes.Dtos;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Models;

namespace PolisProReminder;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(u => u.Role, r=> r.MapFrom(a => a.Role.Name));
        CreateMap<Policy, PolicyDto>();

        CreateMap<Policy, InsurerPolicyDto>()
            .ForMember(p => p.InsuranceCompany, c => c.MapFrom(s => s.InsuranceCompany.ShortName));

        CreateMap<InsuranceCompany, InsuranceCompanyDto>()
            .ReverseMap();
        CreateMap<Insurer, PolicyInsurerDto>()
            .ReverseMap();
        
        CreateMap<InsuranceType, InsuranceTypeDto>()
            .ReverseMap();

        CreateMap<Insurer, InsurerDto>();

        CreateMap<CreateInsurerDto, Insurer>()
            .ReverseMap();
        CreateMap<CreateInsuranceCompanyDto, InsuranceCompany>()
            .ReverseMap();
        
        CreateMap<CreateInsuranceTypeDto, InsuranceType>();
        CreateMap<InsuranceTypeDto, CreateInsuranceTypeDto>();
    }
}
