using FluentValidation;

namespace PolisProReminder.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
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
