using AutoMapper;
using PolisProReminder.Entities;
using PolisProReminder.Models;

namespace PolisProReminder
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Role, r=> r.MapFrom(a => a.Role.Name));
            CreateMap<Policy, PolicyDto>()
                .ForMember(p => p.InsuranceCompany, c => c.MapFrom(s => s.InsuranceCompany.Name));

            CreateMap<Policy, InsurerPolicyDto>()
                .ForMember(p => p.InsuranceCompany, c => c.MapFrom(s => s.InsuranceCompany.Name));

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
            CreateMap<CreatePolicyDto, Insurer>();
            CreateMap<InsuranceTypeDto, CreateInsuranceTypeDto>();
        }
    }
}
