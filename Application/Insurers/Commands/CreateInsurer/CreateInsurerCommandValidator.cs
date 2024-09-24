using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseInsurer;

namespace PolisProReminder.Application.Insurers.Commands.CreateInsurer;

public class CreateInsurerCommandValidator : AbstractValidator<CreateInsurerCommand>
{
    public CreateInsurerCommandValidator()
    {
        Include(new BaseInsurerCommandValidator());
    }
}
