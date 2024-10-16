using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetInsurerById;

public record GetInsurerByIdQuery(Guid Id) : IRequest<BaseInsurerDto>
{
    public Guid Id { get; } = Id;
}
