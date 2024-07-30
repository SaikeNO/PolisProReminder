using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommandHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository,
    IInsurersRepository insurersRepository,
    IVehicleBrandsRepository vehicleBrandsRepository,
    IAttachmentsRepository attachmentsRepository) : IRequestHandler<UpdateVehicleCommand>
{
    public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var vehicleByRegistration = await vehiclesRepository.GetByRegistrationNumber(currentUser.AgentId, request.RegistrationNumber, request.Id);

        if (vehicleByRegistration != null)
            throw new AlreadyExistsException("Pojazd o podanym numerze rejestracyjnym już istnieje");

        var vehicle = await vehiclesRepository.GetById(currentUser.AgentId, request.Id) ?? throw new NotFoundException("Pojazd o podanym ID nie istnieje");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.InsurerId) ?? throw new NotFoundException("Klient o podanym ID nie istnieje");
        var vehicleBrand = await vehicleBrandsRepository.GetById(request.VehicleBrandId) ?? throw new NotFoundException("Marka pojazdu o podanym ID nie istnieje");

        vehicle.FirstRegistrationDate = request.FirstRegistrationDate;
        vehicle.RegistrationNumber = request.RegistrationNumber.ToUpper();
        vehicle.ProductionYear = request.ProductionYear;
        vehicle.Capacity = request.Capacity;
        vehicle.Name = request.Name;
        vehicle.VIN = request.VIN;
        vehicle.KM = request.KM;
        vehicle.KW = request.KW;
        vehicle.Mileage = request.Mileage;
        vehicle.VehicleBrand = vehicleBrand;
        vehicle.Insurer = insurer;

        var savePath = Path.Combine(currentUser.AgentId, request.InsurerId.ToString(), "Vehicles", request.Id.ToString());

        var attachments = request.Attachments.Select(attachment => new Attachment(attachment.FileName, savePath)
        {
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
        }).ToList();

        var attachmentsFormFiles = request.Attachments.Select((attachment, i) => new AttachmentFormFile(attachment, attachments[i].FilePath));
        await attachmentsRepository.UploadAttachmentsAsync(attachmentsFormFiles);

        vehicle.Attachments = [..vehicle.Attachments, ..attachments];

        //await attachmentsRepository.CreateAttachmentRangeAsync(attachments);

        await vehiclesRepository.SaveChanges();
    }
}
