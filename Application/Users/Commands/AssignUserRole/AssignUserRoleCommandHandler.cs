using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException("Użytkownik o podanym adresie email nie istnieje");

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException("Rola o podanej nazwie nie istnieje");

        await userManager.AddToRoleAsync(user, role.Name!);

    }
}