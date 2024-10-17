using AutoMapper;
using PolisProReminder.Application.Insurers.Commands.CreateBusinessInsurer;
using PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurersProfile : Profile
{
    public InsurersProfile()
    {
        CreateMap<CreateIndividualInsurerCommand, IndividualInsurer>();
        CreateMap<CreateBusinessInsurerCommand, BusinessInsurer>();

        CreateMap<BusinessInsurer, BusinessInsurerDto>();
        CreateMap<IndividualInsurer, IndividualInsurerDto>();

        CreateMap<IndividualInsurer, VehicleIndividualInsurerDto>();
        CreateMap<BusinessInsurer, VehicleBusinessInsurerDto>();

        CreateMap<BaseInsurer, InsurerBasicInfoDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetInsurerName(src)))
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

    }

    public static string GetInsurerName(BaseInsurer insurer)
    {
        if (insurer is IndividualInsurer individualInsurer)
        {
            return $"{individualInsurer.FirstName} {individualInsurer.LastName}";
        }
        else if (insurer is BusinessInsurer businessInsurer)
        {
            return businessInsurer.Name;
        }

        return string.Empty;
    }
}
