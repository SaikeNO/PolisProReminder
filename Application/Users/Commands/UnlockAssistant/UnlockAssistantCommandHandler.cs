using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Commands.UnlockAssistant;

internal sealed class UnlockAssistantCommandHandler(IUserContext userContext, IUsersRepository usersRepository) : IRequestHandler<UnlockAssistantCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task Handle(UnlockAssistantCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser()
            ?? throw new NotFoundException("User not found.");

        var assistant = await _usersRepository.GetUserAsync(request.AssistantId, cancellationToken)
            ?? throw new NotFoundException($"Assistant with ID {request.AssistantId} not found.");

        if (assistant.AgentId != user.Id)
        {
            throw new NotAllowedException("You cannot unlock user who is not your assistant");
        }

        if (assistant.LockoutEnd == null)
        {
            throw new NotAllowedException("User is already unlocked.");
        }

        await _usersRepository.UnlockUserAsync(assistant, cancellationToken);
    }
}
