﻿using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

public class GetAllInsurersCommand : IRequest<IEnumerable<InsurerDto>>
{
}
