using FluentValidation;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

public class GetPaginatedInsurersQueryValidator : AbstractValidator<GetPaginatedInsurersQuery>
{
    private readonly int[] allowPageSizes = [5, 10, 15, 30];
    private readonly string[] allowedSortByColumnNames = [
        nameof(InsurerDto.FirstName),
        nameof(InsurerDto.LastName),
        nameof(InsurerDto.Email),
        nameof(InsurerDto.Pesel),
    ];

    public GetPaginatedInsurersQueryValidator()
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
