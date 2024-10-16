using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetPaginatedIndividualInsurers;

public record GetPaginatedIndividualInsurersQuery : PageRequest, IRequest<PageResult<IndividualInsurerDto>> { }
