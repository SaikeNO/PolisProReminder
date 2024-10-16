﻿namespace PolisProReminder.Mailer.RabbitMQ;

public class RabbitMQSettings
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string RoutingKey { get; set; } = string.Empty;
    public string Queue { get; set; } = string.Empty;
}
