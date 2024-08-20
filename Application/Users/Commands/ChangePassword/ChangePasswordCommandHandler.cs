using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Users.Commands.ChangePassword;

internal class ChangePasswordCommandHandler(IUserContext userContext, UserManager<User> userManager) : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var user = await userManager.FindByIdAsync(currentUser.Id);
        _ = user ?? throw new InvalidOperationException("User is not present");
       

        var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new BadRequestException("Błąd podczas zmiany hasła");
        }
    }
}
