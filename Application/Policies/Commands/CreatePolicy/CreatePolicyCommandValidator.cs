using FluentValidation;
using PolisProReminder.Application.Policies.Commands.BasePolicy;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommandValidator : AbstractValidator<CreatePolicyCommand>
{
    public CreatePolicyCommandValidator()
    {
        Include(new BasePolicyCommandValidator());
    }
}
