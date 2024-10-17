using FluentValidation;

namespace PolisProReminder.Application.Vehicles.Commands.BaseVehicle;

public class BaseVehicleCommandValidator : AbstractValidator<BaseVehicleCommand>
{
    public BaseVehicleCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .Length(1, 60);

        RuleFor(dto => dto.VIN)
            .NotEmpty()
            .Length(1, 17);

        RuleFor(dto => dto.RegistrationNumber)
            .MaximumLength(8);

        RuleFor(dto => dto.InsurerIds)
            .NotEmpty();

        RuleFor(dto => dto.VehicleBrandId)
            .NotEmpty();
    }
}
