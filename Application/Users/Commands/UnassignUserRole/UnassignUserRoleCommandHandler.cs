using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommandHandler(UserManager<User> userManager,
    RoleManager<UserRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
{
    public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException("Użytkownik o podanym adresie email nie istnieje");

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException("Rola o podanej nazwie nie istnieje");

        await userManager.RemoveFromRoleAsync(user, role.Name!);

    }
}
