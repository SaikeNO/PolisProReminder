using MediatR;
using PolisProReminder.Application.Policies.Notifications;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.UpdatePolicy;

public class UpdatePolicyCommandHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository,
    IBaseInsurersRepository insurersRepository,
    IInsuranceTypesRepository insuranceTypesRepository,
    IAttachmentsRepository attachmentsRepository,
    IMediator mediator) : IRequestHandler<UpdatePolicyCommand>
{
    public async Task Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var policy = await policiesRepository.GetById(currentUser.AgentId, request.Id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        if (await policiesRepository.GetByNumber(currentUser.AgentId, policy.PolicyNumber, request.PolicyNumber) != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        var newTypes = await insuranceTypesRepository.GetManyByIds(currentUser.AgentId, request.InsuranceTypeIds);
        var newInsurers = await insurersRepository.GetManyByIds(currentUser.AgentId, request.InsurerIds);

        policy.PolicyNumber = request.PolicyNumber;
        policy.InsuranceCompanyId = request.InsuranceCompanyId;
        policy.StartDate = request.StartDate;
        policy.EndDate = request.EndDate;
        policy.PaymentDate = request.PaymentDate;
        policy.IsPaid = request.IsPaid;
        policy.Title = request.Title;
        policy.Note = request.Note;

        policy.Insurers.Clear();
        policy.Insurers.AddRange(newInsurers);

        policy.InsuranceTypes.Clear();
        policy.InsuranceTypes.AddRange(newTypes);

        var savePath = Path.Combine(currentUser.AgentId.ToString(), request.InsurerIds.First().ToString(), "Policies", request.Id.ToString());

        var attachments = request.Attachments.Select(attachment => new Attachment(attachment.FileName, savePath)
        {
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
        }).ToList();

        var attachmentsFormFiles = request.Attachments.Select((attachment, i) => new AttachmentFormFile(attachment, attachments[i].FilePath));
        await attachmentsRepository.UploadAttachmentsAsync(attachmentsFormFiles);

        await attachmentsRepository.CreateAttachmentRangeAsync(attachments);
        policy.Attachments = [.. policy.Attachments, .. attachments];
        await policiesRepository.SaveChanges();

        var notification = new UpdatePolicyNotification(policy);
        await mediator.Publish(notification, cancellationToken);
    }
}
