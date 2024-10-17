using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

public record GetAllInsurersQuery : IRequest<IEnumerable<InsurerBasicInfoDto>> { }
