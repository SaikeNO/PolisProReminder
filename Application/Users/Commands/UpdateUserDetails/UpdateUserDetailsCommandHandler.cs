using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        _ = dbUser ?? throw new NotFoundException("Użytkownik o podanym ID nie istnieje");
        
        dbUser.FirstName = request.FirstName;
        dbUser.LastName = request.LastName;
        dbUser.Email = request.Email;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}