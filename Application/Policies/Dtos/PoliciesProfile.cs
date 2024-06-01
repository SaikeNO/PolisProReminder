using AutoMapper;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Policies.Dtos;

public class PoliciesProfile : Profile
{
    public PoliciesProfile()
    {
        CreateMap<Policy, PolicyDto>();

        CreateMap<Policy, InsurerPolicyDto>()
            .ForMember(p => p.InsuranceCompany, c => c.MapFrom(s => s.InsuranceCompany.ShortName));

    }
}
