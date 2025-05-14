using FluentValidation;

namespace PolisProReminder.Application.Users.Commands.CreateAssistant;

public class CreateAssistantCommandValidator : AbstractValidator<CreateAssistantCommand>
{
    public CreateAssistantCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email address is required.");
    }
}
