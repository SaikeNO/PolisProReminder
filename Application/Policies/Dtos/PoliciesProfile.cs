using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Policies.Dtos;

public class PoliciesProfile : Profile
{
    public PoliciesProfile()
    {
        CreateMap<Policy, PolicyDto>()
            .ForMember(p => p.InsurerId, p => p.MapFrom(p => p.Insurer.Id))
            .ForMember(p => p.InsurerName, p => p.MapFrom(p => GetInsurerName(p.Insurer)));
    }

    private static string GetInsurerName(BaseInsurer insurer)
    {
        return insurer switch
        {
            IndividualInsurer individualInsurer => $"{individualInsurer.LastName} {individualInsurer.FirstName}",
            BusinessInsurer businessInsurer => businessInsurer.Name,
            _ => string.Empty
        };
    }
}
