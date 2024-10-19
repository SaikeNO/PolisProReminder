using FluentValidation;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetPaginatedBusinessInsurers;

internal class GetPaginatedBusinessInsurersQueryValidator : AbstractValidator<GetPaginatedBusinessInsurersQuery>
{
    private readonly int[] allowPageSizes = [10, 20, 30, 50];
    private readonly string[] allowedSortByColumnNames = [
        nameof(BusinessInsurerDto.Name),
        nameof(BusinessInsurerDto.Email),
        nameof(BusinessInsurerDto.Regon),
        nameof(BusinessInsurerDto.Nip),
    ];

    public GetPaginatedBusinessInsurersQueryValidator()
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
