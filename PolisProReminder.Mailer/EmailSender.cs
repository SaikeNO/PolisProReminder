using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Mail;
using PolisProReminder.Mailer.SendEmail;

namespace PolisProReminder.Mailer;

public class EmailSender(IMediator mediator, IConfiguration configuration) : IEmailSender<User>
{
    private readonly IMediator _mediator = mediator;
    private readonly string _frontendUrl = configuration.GetSection("FrontendUrl").Value
        ?? throw new InvalidOperationException("Frontend URL is not configured. Please set 'FrontendUrl' in appsettings.json.");

    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var uri = new Uri(confirmationLink);
        var frontendUri = new Uri(_frontendUrl);
        var newLink = new UriBuilder(uri)
        {
            Scheme = frontendUri.Scheme,
            Host = frontendUri.Host,
            Port = frontendUri.IsDefaultPort ? -1 : frontendUri.Port
        }.Uri.ToString();

        var text = string.Format(MailMessages.ConfirmEmailChanged, user.FirstName, newLink);

        await _mediator.Send(new SendEmailCommand(email, MailSubjects.ConfirmEmailChanged, text));
    }

    public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        await Task.Delay(0); // Placeholder for actual implementation
        throw new NotImplementedException();
    }

    public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        await Task.Delay(0); // Placeholder for actual implementation
        throw new NotImplementedException();
    }
}
