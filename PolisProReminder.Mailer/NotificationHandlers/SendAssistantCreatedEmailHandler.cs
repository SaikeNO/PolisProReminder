using System.Text;
using System.Text.Encodings.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using PolisProReminder.Application.Users.Notifications;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Mail;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Mailer.SendEmail;

namespace PolisProReminder.Mailer.NotificationHandlers;

internal sealed class SendAssistantCreatedEmailHandler(
    UserManager<User> userManager,
    IUsersRepository usersRepository,
    IMediator mediator,
    IConfiguration configuration) : INotificationHandler<AssistantCreatedNotification>
{
    private readonly IMediator _mediator = mediator;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly UserManager<User> _userManager = userManager;
    private readonly string _frontendUrl = configuration.GetSection("FrontendUrl").Value
            ?? throw new InvalidOperationException("Frontend URL is not configured. Please set 'FrontendUrl' in appsettings.json.");

    public async Task Handle(AssistantCreatedNotification notification, CancellationToken cancellationToken)
    {
        var assistant = await _usersRepository.GetUserAsync(notification.UserId, cancellationToken)
            ?? throw new InvalidOperationException("Assistant with given ID does not exist");

        var passwordResetCode = await GenerateEncodedTokenAsync(() => _userManager.GeneratePasswordResetTokenAsync(assistant));
        var emailConfirmationCode = await GenerateEncodedTokenAsync(() => _userManager.GenerateEmailConfirmationTokenAsync(assistant));

        var url = BuildUrl(new RouteValueDictionary
        {
            ["userId"] = assistant.Id,
            ["userEmail"] = assistant.Email,
            ["passwordResetCode"] = passwordResetCode,
            ["emailConfirmationCode"] = emailConfirmationCode
        });

        var encodedUrl = HtmlEncoder.Default.Encode(url);
        var text = string.Format(MailMessages.AssistantCreated, assistant.FirstName, assistant.UserName, encodedUrl);
        var sendEmailCommand = new SendEmailCommand(assistant.Email!, MailSubjects.AssistantCreated, text);

        await _mediator.Send(sendEmailCommand, cancellationToken);
    }

    private static async Task<string> GenerateEncodedTokenAsync(Func<Task<string>> tokenGenerator)
    {
        var token = await tokenGenerator();
        return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
    }

    private string BuildUrl(RouteValueDictionary routeValues)
    {
        var queryString = string.Join("&", routeValues
            .Where(kv => kv.Value != null)
            .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value?.ToString() ?? string.Empty)}"));

        return $"{_frontendUrl}/auth/setPassword?{queryString}";
    }
}
