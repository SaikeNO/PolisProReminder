using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommandHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository,
    IInsurersRepository insurersRepository,
    IVehicleBrandsRepository vehicleBrandsRepository) : IRequestHandler<UpdateVehicleCommand>
{
    public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var vehicle = await vehiclesRepository.GetById(currentUser.AgentId, request.Id) ?? throw new NotFoundException("Pojazd o podanym numerze rejestracyjnym nie istnieje");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.InsurerId) ?? throw new NotFoundException("Pojazd o podanym ID nie istnieje");
        var vehicleBrand = await vehicleBrandsRepository.GetById(request.VehicleBrandId) ?? throw new NotFoundException("Marka pojazdu o podanym ID nie istnieje");

        vehicle.FirstRegistrationDate = request.FirstRegistrationDate;
        vehicle.RegistrationNumber = request.RegistrationNumber;
        vehicle.ProductionYear = request.ProductionYear;
        vehicle.Capacity = request.Capacity;
        vehicle.Name = request.Name;
        vehicle.VIN = request.VIN;
        vehicle.KM = request.KM;
        vehicle.KW = request.KW;
        vehicle.Mileage = request.Mileage;
        vehicle.VehicleBrand = vehicleBrand;
        vehicle.Insurer = insurer;

        await vehiclesRepository.SaveChanges();
    }
}
