using FluentValidation;

namespace PolisProReminder.Application.Policies.Commands.BasePolicy;

public class BasePolicyCommandValidator : AbstractValidator<BasePolicyCommand>
{
    public BasePolicyCommandValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .Length(1, 60);

        RuleFor(dto => dto.PolicyNumber)
            .NotEmpty()
            .Length(1, 60);

        RuleFor(dto => dto.Note)
            .NotEmpty()
            .Length(1, 500);

        RuleFor(dto => dto.InsuranceCompanyId)
            .NotEmpty();

        RuleFor(dto => dto.InsurerIds)
            .NotEmpty();
    }
}
