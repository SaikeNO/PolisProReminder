using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetIndividualInsurerById;

public record GetIndividualInsurerByIdQuery(Guid Id) : IRequest<IndividualInsurerDto>
{
    public Guid Id { get; } = Id;
}
