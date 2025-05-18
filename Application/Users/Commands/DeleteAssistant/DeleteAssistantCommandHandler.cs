using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Commands.DeleteAssistant;

internal sealed class DeleteAssistantCommandHandler(IUserContext userContext, IUsersRepository usersRepository) : IRequestHandler<DeleteAssistantCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task Handle(DeleteAssistantCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser()
            ?? throw new NotFoundException("User not found.");

        var assistant = await _usersRepository.GetUserAsync(request.AssistantId, cancellationToken)
            ?? throw new NotFoundException($"Assistant with ID {request.AssistantId} not found.");

        if (assistant.AgentId != user.Id)
        {
            throw new NotAllowedException("You cannot delete user who is not your assistant");
        }

        if (assistant.IsDeleted)
        {
            throw new NotAllowedException("User is already deleted.");
        }

        await _usersRepository.DeleteUserAsync(assistant, cancellationToken);
    }
}
