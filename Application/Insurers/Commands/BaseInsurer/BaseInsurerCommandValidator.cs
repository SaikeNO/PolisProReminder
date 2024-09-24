using FluentValidation;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Insurers.Commands.BaseInsurer;
public class BaseInsurerCommandValidator : AbstractValidator<BaseInsurerCommand>
{
    public BaseInsurerCommandValidator()
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

        RuleFor(dto => dto.PostalCode)
            .Length(6);

        RuleFor(dto => dto.Street)
            .MaximumLength(60);

        RuleFor(dto => dto.City)
            .MaximumLength(60);

        RuleFor(dto => dto.PhoneNumber)
            .Matches(Resources.PhoneRegex);
    }
}
