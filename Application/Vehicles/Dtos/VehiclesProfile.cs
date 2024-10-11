using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehiclesProfile : Profile
{
    public VehiclesProfile()
    {
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(v => v.InsurerId, v => v.MapFrom(v => v.Insurer.Id))
            .ForMember(v => v.InsurerName, v => v.MapFrom(v => GetInsurerName(v.Insurer)));
        CreateMap<IndividualInsurer, VehicleIndividualInsurerDto>();
        CreateMap<BusinessInsurer, VehicleBusinessInsurerDto>();
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
