using AutoMapper;
using PolisProReminder.Entities;
using PolisProReminder.Models;

namespace PolisProReminder
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InsurancePolicy, InsurancePolicyDto>()
                .ForMember(p => p.InsuranceCompany, c => c.MapFrom(s => s.InsuranceCompany.Name));

            CreateMap<InsuranceType, InsuranceTypeDto>();
            CreateMap<Insurer, InsurerDto>();
        }
    }
}
