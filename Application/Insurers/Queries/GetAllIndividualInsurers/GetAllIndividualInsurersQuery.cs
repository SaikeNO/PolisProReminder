using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetAllIndividualInsurers;

public class GetAllIndividualInsurersQuery : IRequest<IEnumerable<IndividualInsurerDto>>
{
}
