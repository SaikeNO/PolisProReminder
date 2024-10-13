using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseInsurer;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Insurers.Commands.BaseIndividualInsurer;

public class BaseIndividualInsurerCommandValidator : AbstractValidator<BaseIndividualInsurerCommand>
{
    public BaseIndividualInsurerCommandValidator()
    {
        Include(new BaseInsurerCommandValidator());

        RuleFor(dto => dto.Pesel)
            .Matches(Resources.PeselRegex)
            .NotEmpty();

        RuleFor(dto => dto.FirstName)
            .NotEmpty()
            .Length(1, 30);

        RuleFor(dto => dto.LastName)
            .MaximumLength(30);
    }
}
