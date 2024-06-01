using FluentValidation;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommandValidator : AbstractValidator<CreatePolicyCommand>
{
    public CreatePolicyCommandValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .Length(1, 60);

        RuleFor(dto => dto.PolicyNumber)
            .NotEmpty()
            .Length(1, 60);

        RuleFor(dto => dto.InsuranceCompanyId)
            .NotEmpty();

        RuleFor(dto => dto.InsurerId)
            .NotEmpty();
    }
}
