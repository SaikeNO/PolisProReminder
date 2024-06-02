using FluentValidation;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Insurers.Commands.CreateInsurer;

public class CreateInsurerCommandValidator : AbstractValidator<CreateInsurerCommand>
{
    public CreateInsurerCommandValidator()
    {
        RuleFor(dto => dto.Pesel)
            .Matches(Resources.PeselRegex)
            .NotEmpty();

        RuleFor(dto => dto.FirstName)
            .NotEmpty()
            .Length(1, 30);

        RuleFor(dto => dto.LastName)
            .MaximumLength(30);

        RuleFor(dto => dto.Email)
            .EmailAddress();

        RuleFor(dto => dto.PhoneNumber)
            .Matches(Resources.PhoneRegex);

    }
}
