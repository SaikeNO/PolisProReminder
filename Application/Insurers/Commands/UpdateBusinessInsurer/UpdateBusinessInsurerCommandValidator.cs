using FluentValidation;
using PolisProReminder.Application.Insurers.Commands.BaseBusinessInsurer;

namespace PolisProReminder.Application.Insurers.Commands.UpdateBusinessInsurer;

public class UpdateBusinessInsurerCommandValidator : AbstractValidator<UpdateBusinessInsurerCommand>
{
    public UpdateBusinessInsurerCommandValidator()
    {
        Include(new BaseBusinessInsurerCommandValidator());

        RuleFor(c => c.Id)
            .NotEmpty();
    }
}