using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using PolisProReminder.Mailer.RabbitMQ;

namespace PolisProReminder.Mailer.SendEmail;

internal class SendEmailCommandHandler(RabbitMQSettings settings,
    IRabbitMQConnection rabbitMQConnection,
    ILogger<SendEmailCommandHandler> logger) : IRequestHandler<SendEmailCommand>
{
    private readonly RabbitMQSettings _settings = settings;
    private readonly IRabbitMQConnection _rabbitMQConnection = rabbitMQConnection;
    private readonly ILogger<SendEmailCommandHandler> _logger = logger;

    public Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        using var channel = _rabbitMQConnection.CreateChannel();
        _rabbitMQConnection.ConfigChannel(channel);

        var messageBody = JsonConvert.SerializeObject(request);
        var body = Encoding.UTF8.GetBytes(messageBody);

        channel.BasicPublish(
            exchange: _settings.Exchange,
            routingKey: _settings.RoutingKey,
            basicProperties: null,
            body: body);

        _logger.LogTrace($"[x] Wysłano wiadomość: {request}");

        return Task.CompletedTask;
    }
}
