using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehiclesProfile : Profile
{
    public VehiclesProfile()
    {
        CreateMap<Vehicle, VehicleDto>();
             //.ForMember(v => v.Insurer, v => v.MapFrom(s => s.InsuranceCompany.ShortName)); ;
        CreateMap<IndividualInsurer, VehicleIndividualInsurerDto>();
        CreateMap<BusinessInsurer, VehicleBusinessInsurerDto>();
    }
}
