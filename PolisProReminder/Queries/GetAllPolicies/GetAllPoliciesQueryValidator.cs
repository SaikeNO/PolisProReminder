﻿using FluentValidation;
using PolisProReminder.Models;

namespace PolisProReminder.Queries.GetAllPolicies;

public class GetAllPoliciesQueryValidator : AbstractValidator<GetAllPoliciesQuery>
{
    private readonly int[] allowPageSizes = [5, 10, 15, 30];
    private readonly string[] allowedSortByColumnNames = [
        nameof(PolicyDto.Title),
        nameof(PolicyDto.PolicyNumber),
        nameof(PolicyDto.EndDate),
        nameof(PolicyDto.StartDate),
        nameof(PolicyDto.PaymentDate)
    ];

    public GetAllPoliciesQueryValidator()
    {
        RuleFor(p => p.PageIndex)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Dopuszczalna ilość stron to {string.Join(", ", allowPageSizes)}");

        RuleFor(p => p.SortBy)
           .Must(value => allowedSortByColumnNames.Contains(value))
           .When(q => q.SortBy != null)
           .WithMessage($"Sortowanie jest opcjonalne i może być tylko dla {string.Join(", ", allowPageSizes)}");
    }
}
