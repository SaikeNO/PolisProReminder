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

            CreateMap<Policy, Models.Insurer.InsurerPolicyDto>()
                .ForMember(p => p.InsuranceCompany, c => c.MapFrom(s => s.InsuranceCompany.Name));

            CreateMap<InsuranceType, InsuranceTypeDto>();
            CreateMap<Insurer, Models.InsurancePolicy.PolicyInsurerDto>();
            CreateMap<Insurer, InsurerDto>();
        }
    }
}
