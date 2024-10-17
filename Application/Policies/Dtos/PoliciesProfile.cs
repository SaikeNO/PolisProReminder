using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Policies.Dtos;

public class PoliciesProfile : Profile
{
    public PoliciesProfile()
    {
        CreateMap<Policy, PolicyDto>();
      
        CreateMap<Policy, InsurerPolicyDto>();
    }
}
