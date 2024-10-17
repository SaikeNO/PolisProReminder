using FluentValidation;
using PolisProReminder.Application.Vehicles.Commands.BaseVehicle;

namespace PolisProReminder.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        Include(new BaseVehicleCommandValidator());
    }
}
