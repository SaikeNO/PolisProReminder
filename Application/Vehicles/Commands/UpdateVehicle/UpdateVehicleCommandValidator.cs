using FluentValidation;
using PolisProReminder.Application.Vehicles.Commands.BaseVehicle;

namespace PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        Include(new BaseVehicleCommandValidator());

        RuleFor(dto => dto.Id)
            .NotEmpty();
    }
}
