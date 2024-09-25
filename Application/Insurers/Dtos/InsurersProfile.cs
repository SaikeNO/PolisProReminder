﻿using AutoMapper;
using PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurersProfile : Profile
{
    public InsurersProfile()
    {
        CreateMap<CreateIndividualInsurerCommand, Insurer>();

        CreateMap<Insurer, PolicyInsurerDto>()
            .ReverseMap();

        CreateMap<Insurer, InsurerDto>();
    }

}
