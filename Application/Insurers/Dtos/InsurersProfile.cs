using AutoMapper;
using PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurersProfile : Profile
{
    public InsurersProfile()
    {
        CreateMap<CreateIndividualInsurerCommand, IndividualInsurer>();

        CreateMap<BusinessInsurer, BusinessInsurerDto>();
        CreateMap<IndividualInsurer, IndividualInsurerDto>();

        CreateMap<IndividualInsurer, VehicleIndividualInsurerDto>();
        CreateMap<BusinessInsurer, VehicleBusinessInsurerDto>();

    }
}
