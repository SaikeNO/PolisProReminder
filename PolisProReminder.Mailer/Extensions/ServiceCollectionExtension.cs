using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Mailer.RabbitMQ;

namespace PolisProReminder.Mailer.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddMailer(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        var rabbitMQSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>() ?? throw new InvalidOperationException("RabbitMQSettings is required");
        services.AddSingleton(rabbitMQSettings);

        services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();

        services.AddTransient<IEmailSender<User>, EmailSender>();
    }
}
