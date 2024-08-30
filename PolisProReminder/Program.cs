using PolisProReminder.API.Extensions;
using PolisProReminder.Application.Extensions;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Seeders;
using PolisProReminder.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    await seeder.Seed();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PolisPro Reminder API");
    });
}
else
{
    await seeder.SeedDeployment();
}

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseCors("frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
