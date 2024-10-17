using FluentValidation;
using PolisProReminder.Application.Policies.Commands.BasePolicy;

namespace PolisProReminder.Application.Policies.Commands.UpdatePolicy;

public class UpdatePolicyCommandValidator : AbstractValidator<UpdatePolicyCommand>
{
    public UpdatePolicyCommandValidator()
    {
        Include(new BasePolicyCommandValidator());

        RuleFor(dto => dto.Id)
            .NotEmpty();
    }
}
