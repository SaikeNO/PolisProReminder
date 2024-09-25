using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseIndividualInsurer;

namespace PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;

public class CreateIndividualInsurerValidator : AbstractValidator<CreateIndividualInsurerCommand>
{
    public CreateIndividualInsurerValidator()
    {
        Include(new BaseIndividualInsurerCommandValidator());
    }
}
