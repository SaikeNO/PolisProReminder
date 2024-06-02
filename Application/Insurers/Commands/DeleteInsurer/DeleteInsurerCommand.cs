using MediatR;

namespace PolisProReminder.Application.Insurers.Commands.DeleteInsurer;

public class DeleteInsurerCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
