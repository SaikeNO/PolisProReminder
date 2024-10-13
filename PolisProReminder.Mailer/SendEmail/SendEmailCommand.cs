using MediatR;

namespace PolisProReminder.Mailer.SendEmail;

public record SendEmailCommand(string To, string Subject, string Text) : IRequest
{
    public string To { get; set; } = To;
    public string Subject { get; set; } = Subject;
    public string Text { get; set; } = Text;
}