using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.UpdateInurer;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Insurers.Commands.UpdateInsurer;

public class UpdateInsurerCommandValidator : AbstractValidator<UpdateInsurerCommand>
{
    public UpdateInsurerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

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
