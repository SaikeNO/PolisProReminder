using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseInsurer;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Insurers.Commands.BaseBusinessInsurer;

public class BaseBusinessInsurerCommandValidator : AbstractValidator<BaseBusinessInsurerCommand>
{
    public BaseBusinessInsurerCommandValidator()
    {
        Include(new BaseInsurerCommandValidator());

        RuleFor(dto => dto.Nip)
            .Matches(Resources.NipRegex);

        RuleFor(dto => dto.Regon)
            .Matches(Resources.RegonRegex);

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .Length(1, 100);
    }
}
