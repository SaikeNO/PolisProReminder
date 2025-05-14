using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Application.Users.Notifications;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Mail;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Mailer.SendEmail;

namespace PolisProReminder.Mailer.NotificationHandlers;

internal class SendAssistantCreatedEmailHandler(UserManager<User> userManager, IUsersRepository usersRepository, IMediator mediator) : INotificationHandler<AssistantCreatedNotification>
{
    private readonly IMediator _mediator = mediator;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly UserManager<User> _userManager = userManager;

    public async Task Handle(AssistantCreatedNotification notification, CancellationToken cancellationToken)
    {
        var assistant = await _usersRepository.GetUserAsync(notification.UserId, cancellationToken)
            ?? throw new InvalidOperationException("Assistant with given ID does not exists");

        var token = await _userManager.GeneratePasswordResetTokenAsync(assistant);

        var encodedToken = WebUtility.UrlEncode(token);
        var callbackUrl = $"https://twojastrona.pl/set-password?userId={assistant.Id}&token={encodedToken}";

        var text = string.Format(MailMessages.AssistantCreated, assistant.FirstName, assistant.UserName, callbackUrl);
        var sendEmailCommand = new SendEmailCommand(assistant.Email!, MailSubjects.AssistantCreated, text);

        await _mediator.Send(sendEmailCommand, cancellationToken);
    }
}
