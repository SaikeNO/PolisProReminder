using MediatR;
using Microsoft.Extensions.Logging;
using PolisProReminder.Application.Policies.Notifications;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Mail;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Mailer.SendEmail;

namespace PolisProReminder.Mailer.NotificationHandlers;

internal class SendPolicyCreatedEmailHandler(IUserContext userContext,
    IUsersRepository usersRepository,
    IMediator mediator,
    ILogger<SendPolicyCreatedEmailHandler> logger) : INotificationHandler<PolicyCreatedNotification>
{
    private readonly IMediator _mediator = mediator;
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<SendPolicyCreatedEmailHandler> _logger = logger;

    public async Task Handle(PolicyCreatedNotification notification, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var agent = await _usersRepository.GetUserAsync(currentUser.AgentId, cancellationToken)
            ?? throw new NotFoundException($"Assistant with ID {currentUser.AgentId} not found.");

        var email = agent.Email;
        if (email == null)
        {
            _logger.LogError("Email agenta: {0} nie istnieje; Użytkownik: {1}", agent.Id, currentUser.Id);
            return;
        }

        var text = string.Format(MailMessages.PolicyCreated, notification.Policy.PolicyNumber, currentUser.Email);
        var sendEmailCommand = new SendEmailCommand(email, MailSubjects.PolicyCreated, text);
        await _mediator.Send(sendEmailCommand, cancellationToken);
    }
}
