using AutoMapper;
using PolisProReminder.Entities;
using PolisProReminder.Models;
using PolisProReminder.Models.InsurancePolicy;
using PolisProReminder.Models.Insurer;

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

            CreateMap<InsuranceCompany, InsuranceCompanyDto>();
            CreateMap<InsuranceType, InsuranceTypeDto>();
            CreateMap<Insurer, PolicyInsurerDto>();
            CreateMap<Insurer, InsurerDto>();

            CreateMap<CreateInsurerDto, Insurer>();
        }
    }
}
