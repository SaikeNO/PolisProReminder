﻿using FluentValidation;

namespace PolisProReminder.Application.Policies.Commands.UpdatePolicy;

public class UpdatePolicyCommandValidator : AbstractValidator<UpdatePolicyCommand>
{
    public UpdatePolicyCommandValidator()
    {
        RuleFor(dto => dto.Id)
            .NotEmpty();

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

        RuleFor(dto => dto.InsurerId)
            .NotEmpty();
    }
}
