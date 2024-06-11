using FluentValidation;
using PolisProReminder.Application.Vehicles.Dtos;

namespace PolisProReminder.Application.Vehicles.Queries.GetAllVehicles;

public class GetAllVehiclesValidator : AbstractValidator<GetAllVehiclesQuery>
{
    private readonly int[] allowPageSizes = [5, 10, 15, 30];
    private readonly string[] allowedSortByColumnNames = [
        nameof(VehicleDto.Name),
        nameof(VehicleDto.VIN),
        nameof(VehicleDto.RegistrationNumber),
        nameof(VehicleDto.FirstRegistrationDate)
    ];

    public GetAllVehiclesValidator()
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
