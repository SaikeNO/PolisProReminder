using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseBusinessInsurer;

namespace PolisProReminder.Application.Insurers.Commands.CreateBusinessInsurer;

public class CreateBusinessInsurerValidator : AbstractValidator<CreateBusinessInsurerCommand>
{
    public CreateBusinessInsurerValidator()
    {
        Include(new BaseBusinessInsurerCommandValidator());
    }
}
