using RabbitMQ.Client;

namespace PolisProReminder.Mailer.RabbitMQ;

public class RabbitMQConnection : IRabbitMQConnection
{
    private readonly RabbitMQSettings _settings;
    private IConnection _connection;
    private readonly object _lock = new();

    public RabbitMQConnection(RabbitMQSettings settings)
    {
        _settings = settings;
        _connection = CreateConnection();
    }

    private IConnection CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };
        return factory.CreateConnection();
    }

    public IModel CreateChannel()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            lock (_lock)
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    _connection = CreateConnection();
                }
            }
        }

        return _connection.CreateModel();
    }

    public void ConfigChannel(IModel channel)
    {
        channel.ExchangeDeclare(_settings.Exchange, ExchangeType.Direct);
        channel.QueueDeclare(_settings.Queue, true, false, false, null);
        channel.QueueBind(_settings.Queue, _settings.Exchange, _settings.RoutingKey, null);
    }
}
