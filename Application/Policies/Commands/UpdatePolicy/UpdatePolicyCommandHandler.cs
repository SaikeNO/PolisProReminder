using MediatR;
using PolisProReminder.Application.Policies.Notifications;
using PolisProReminder.Application.Users;
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
        var attachments = await attachmentsRepository.GetManyByIds(request.AttachmentIds);

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

        policy.Attachments = [.. policy.Attachments, .. attachments];

        await policiesRepository.SaveChanges();

        var notification = new UpdatePolicyNotification(policy);
        await mediator.Publish(notification, cancellationToken);
    }
}
