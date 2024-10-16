using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetAllBusinessInsurers;

public class GetAllBusinessInsurersQuery : IRequest<IEnumerable<BusinessInsurerDto>>
{
}
