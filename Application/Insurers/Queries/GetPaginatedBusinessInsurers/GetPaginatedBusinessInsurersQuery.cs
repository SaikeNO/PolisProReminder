using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetPaginatedBusinessInsurers;

public record GetPaginatedBusinessInsurersQuery : PageRequest, IRequest<PageResult<BusinessInsurerDto>> { }
