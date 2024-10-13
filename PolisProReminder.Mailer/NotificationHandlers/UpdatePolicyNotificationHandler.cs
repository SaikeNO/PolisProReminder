using MediatR;
using Microsoft.Extensions.Logging;
using PolisProReminder.Application.Policies.Notifications;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Mails;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Mailer.SendEmail;

namespace PolisProReminder.Mailer.NotificationHandlers;

internal class UpdatePolicyNotificationHandler(IUserContext userContext,
    IUsersRepository usersRepository,
    IMediator mediator,
    ILogger<UpdatePolicyNotificationHandler> logger) : INotificationHandler<UpdatePolicyNotification>
{
    private readonly IMediator _mediator = mediator;
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<UpdatePolicyNotificationHandler> _logger = logger;

    public async Task Handle(UpdatePolicyNotification notification, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var agent = await _usersRepository.GetAgentAsync(currentUser.Id);

        var email = agent.Email;
        if (email == null)
        {
            _logger.LogError("Email agenta: {0} nie istnieje; Użytkownik: {1}", agent.Id, currentUser.Id);
            return;
        }

        var text = string.Format(MailMessages.PolicyUpdated, notification.Policy.PolicyNumber, currentUser.Email);
        var sendEmailCommand = new SendEmailCommand(email, MailSubjects.PolicyUpdated, text);
        await _mediator.Send(sendEmailCommand, cancellationToken);
    }
}
