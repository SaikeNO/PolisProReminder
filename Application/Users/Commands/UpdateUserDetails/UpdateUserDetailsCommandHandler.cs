using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUserStore<User> _userStore = userStore;
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
        _ = user ?? throw new InvalidOperationException("Current User is not present");

        var dbUser = await _userStore.FindByIdAsync(user.Id.ToString(), cancellationToken);

        _ = dbUser ?? throw new NotFoundException("Użytkownik o podanym ID nie istnieje");
        
        dbUser.FirstName = request.FirstName;
        dbUser.LastName = request.LastName;

        await _userStore.UpdateAsync(dbUser, cancellationToken);
    }
}