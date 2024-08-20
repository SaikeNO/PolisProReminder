using MediatR;

namespace PolisProReminder.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
