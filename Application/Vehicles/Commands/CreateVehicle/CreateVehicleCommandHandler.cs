using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommandHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository,
    IBaseInsurersRepository insurersRepository,
    IVehicleBrandsRepository vehicleBrandsRepository,
    IAttachmentsRepository attachmentsRepository) : IRequestHandler<CreateVehicleCommand, Guid>
{
    public async Task<Guid> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var vehicle = await vehiclesRepository.GetByRegistrationNumber(currentUser.AgentId, request.RegistrationNumber, null);

        if (vehicle != null)
            throw new AlreadyExistsException("Pojazd o podanym numerze rejestracyjnym już istnieje");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.InsurerId) ?? throw new NotFoundException("Klient o podanym ID nie istnieje");
        var vehicleBrand = await vehicleBrandsRepository.GetById(request.VehicleBrandId) ?? throw new NotFoundException("Marka pojazdu o podanym ID nie istnieje");

        var createVehicle = new Vehicle()
        {
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
            FirstRegistrationDate = request.FirstRegistrationDate,
            RegistrationNumber = request.RegistrationNumber.ToUpper(),
            ProductionYear = request.ProductionYear,
            Capacity = request.Capacity,
            Name = request.Name,
            VIN = request.VIN,
            KM = request.KM,
            KW = request.KW,
            Mileage = request.Mileage,
            VehicleBrand = vehicleBrand,
            Insurer = insurer,
        };

        var savePath = Path.Combine(currentUser.AgentId.ToString(), request.InsurerId.ToString(), "Vehicles", createVehicle.Id.ToString());

        var attachments = request.Attachments.Select(attachment => new Attachment(attachment.FileName, savePath)
        {
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
        }).ToList();

        var attachmentsFormFiles = request.Attachments.Select((attachment, i) => new AttachmentFormFile(attachment, attachments[i].FilePath));
        await attachmentsRepository.UploadAttachmentsAsync(attachmentsFormFiles);

        createVehicle.Attachments = attachments;

        var id = await vehiclesRepository.Create(createVehicle);
        return id;
    }
}
