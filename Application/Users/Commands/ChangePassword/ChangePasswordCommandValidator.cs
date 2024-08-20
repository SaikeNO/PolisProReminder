using FluentValidation;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(dto => dto.CurrentPassword)
           .NotEmpty();

        RuleFor(dto => dto.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(Resources.PasswordRegex);
    }
}
