using FluentValidation;
using PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;

namespace PolisProReminder.Application.Vehicles.Commands.CreateVehicle;

public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .Length(1, 60);

        RuleFor(dto => dto.VIN)
            .NotEmpty()
            .Length(1, 17);

        RuleFor(dto => dto.RegistrationNumber)
            .MaximumLength(8);

        RuleFor(dto => dto.InsurerId)
            .NotEmpty();
        
        RuleFor(dto => dto.VehicleBrandId)
            .NotEmpty();
    }
}
