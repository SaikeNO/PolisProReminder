using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Application.Users.Notifications;
using PolisProReminder.Domain.Constants;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Users.Commands.CreateAssistant;

internal sealed class CreateAssistantCommandHandler(
    IUserContext userContext,
    UserManager<User> userManager,
    RoleManager<UserRole> roleManager,
    IMediator mediator) : IRequestHandler<CreateAssistantCommand, Guid>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<UserRole> _roleManager = roleManager;
    private readonly IUserContext _userContext = userContext;
    private readonly IMediator _mediator = mediator;

    public async Task<Guid> Handle(CreateAssistantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
            ?? throw new InvalidOperationException("Current User is not present");

        var newAssistant = new User
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            AgentId = currentUser.Id,
        };

        var role = await _roleManager.FindByNameAsync(UserRoles.Assistant)
            ?? throw new NotFoundException("Rola o podanej nazwie nie istnieje");

        var result = await _userManager.CreateAsync(newAssistant);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Nie można utworzyć asystenta: {errors}");
        }

        result = await _userManager.AddToRoleAsync(newAssistant, role.Name!);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Nie można przypisać roli asystenta: {errors}");
        }

        var assistantCreatedNotification = new AssistantCreatedNotification(newAssistant.Id);
        await _mediator.Publish(assistantCreatedNotification, cancellationToken);

        return newAssistant.Id;
    }
}
