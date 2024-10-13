using RabbitMQ.Client;

namespace PolisProReminder.Mailer.RabbitMQ;

public interface IRabbitMQConnection
{
    IModel CreateChannel();
    void ConfigChannel(IModel channel);
}