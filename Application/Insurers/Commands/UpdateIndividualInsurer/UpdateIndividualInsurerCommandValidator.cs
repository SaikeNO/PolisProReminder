using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseInsurer;

namespace PolisProReminder.Application.Insurers.Commands.UpdateIndividualInsurer;

public class UpdateIndividualInsurerCommandValidator : AbstractValidator<UpdateIndividualInsurerCommand>
{
    public UpdateIndividualInsurerCommandValidator()
    {
        Include(new BaseInsurerCommandValidator());

        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
