using MediatR;

namespace PolisProReminder.Application.Insurers.Commands.DeleteIndividualInsurer;

public class DeleteIndividualInsurerCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
