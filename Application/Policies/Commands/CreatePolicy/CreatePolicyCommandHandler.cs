using MediatR;
using PolisProReminder.Application.Policies.Notifications;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.CreatePolicy;

public class CreatePolicyCommandHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository,
    IInsuranceTypesRepository insuranceTypesRepository,
    IBaseInsurersRepository insurersRepository,
    IAttachmentsRepository attachmentsRepository,
    IMediator mediator) : IRequestHandler<CreatePolicyCommand, Guid>
{
    public async Task<Guid> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var policy = await policiesRepository.GetByNumber(currentUser.AgentId, request.PolicyNumber);

        if (policy != null)
            throw new AlreadyExistsException("Polisa o podanym numerze już istnieje");

        var types = await insuranceTypesRepository.GetManyByIds(currentUser.AgentId, request.InsuranceTypeIds);
        var insurers = await insurersRepository.GetManyByIds(currentUser.AgentId, request.InsurerIds);
        var attachments = await attachmentsRepository.GetManyByIds(request.AttachmentIds);

        var createPolicy = new Policy
        {
            PolicyNumber = request.PolicyNumber,
            InsuranceCompanyId = request.InsuranceCompanyId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PaymentDate = request.PaymentDate,
            Insurers = insurers.ToList(),
            InsuranceTypes = types.ToList(),
            IsPaid = request.IsPaid,
            Title = request.Title,
            Note = request.Note,
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id,
            Attachments = attachments.ToList()
        };

        var id = await policiesRepository.Create(createPolicy);

        var notification = new CreatePolicyNotification(createPolicy);
        await mediator.Publish(notification, cancellationToken);

        return id;
    }
}
