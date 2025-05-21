using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Commands.UpdateUserDetails;

internal sealed class UpdateUserDetailsCommandHandler(IUserContext userContext, IUsersRepository usersRepository) : IRequestHandler<UpdateUserDetailsCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;
    
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser()
            ?? throw new InvalidOperationException("Current User is not present");

        var dbUser = await _usersRepository.GetUserAsync(user.Id, cancellationToken)
            ?? throw new NotFoundException("Użytkownik o podanym ID nie istnieje");

        dbUser.FirstName = request.FirstName;
        dbUser.LastName = request.LastName;

        await _usersRepository.UpdateUser(dbUser);
        await _usersRepository.SaveChangesAsync(cancellationToken);
    }
}
