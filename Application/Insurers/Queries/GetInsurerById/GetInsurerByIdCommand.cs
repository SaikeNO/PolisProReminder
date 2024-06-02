using MediatR;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetInsurerById;

public class GetInsurerByIdCommand(Guid id) : IRequest<InsurerDto>
{
    public Guid Id { get; } = id;
}
