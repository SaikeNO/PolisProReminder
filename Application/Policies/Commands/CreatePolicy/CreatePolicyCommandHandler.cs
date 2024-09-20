using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommandHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository,
    IInsuranceTypesRepository insuranceTypesRepository,
    IAttachmentsRepository attachmentsRepository) : IRequestHandler<CreatePolicyCommand, Guid>
{
    public async Task<Guid> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var policy = await policiesRepository.GetByNumber(currentUser.AgentId, request.PolicyNumber);

        if (policy != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        var types = await insuranceTypesRepository.GetManyByIds(currentUser.AgentId, request.InsuranceTypeIds);

        var createPolicy = new Policy
        {
            PolicyNumber = request.PolicyNumber,
            InsuranceCompanyId = request.InsuranceCompanyId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PaymentDate = request.PaymentDate,
            InsurerId = request.InsurerId,
            InsuranceTypes = types.ToList(),
            IsPaid = request.IsPaid,
            Title = request.Title,
            Note = request.Note,
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
        };

        var savePath = Path.Combine(currentUser.AgentId, request.InsurerId.ToString(), "Policies", createPolicy.Id.ToString());

        var attachments = request.Attachments.Select(attachment => new Attachment(attachment.FileName, savePath)
        {
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
        }).ToList();

        var attachmentsFormFiles = request.Attachments.Select((attachment, i) => new AttachmentFormFile(attachment, attachments[i].FilePath));
        await attachmentsRepository.UploadAttachmentsAsync(attachmentsFormFiles);

        createPolicy.Attachments = attachments;

        var id = await policiesRepository.Create(createPolicy);
        return id;
    }
}
