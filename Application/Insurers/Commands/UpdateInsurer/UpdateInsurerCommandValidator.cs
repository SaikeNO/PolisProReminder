using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseInsurer;

namespace PolisProReminder.Application.Insurers.Commands.UpdateInsurer;

public class UpdateInsurerCommandValidator : AbstractValidator<UpdateInsurerCommand>
{
    public UpdateInsurerCommandValidator()
    {
        Include(new BaseInsurerCommandValidator());

        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
