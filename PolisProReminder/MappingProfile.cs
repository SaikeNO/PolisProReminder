using AutoMapper;
using PolisProReminder.Entities;
using PolisProReminder.Models;

namespace PolisProReminder
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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

            CreateMap<CreateInsurerDto, Insurer>();
            CreateMap<CreateInsuranceCompanyDto, InsuranceCompany>();
            CreateMap<CreateInsuranceTypeDto, InsuranceType>();
            CreateMap<CreatePolicyDto, Insurer>();
            CreateMap<InsuranceTypeDto, CreateInsuranceTypeDto>();
        }
    }
}
