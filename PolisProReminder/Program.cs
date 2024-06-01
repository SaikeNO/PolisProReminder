using Microsoft.AspNetCore.Authorization;
using PolisProReminder.API.Extensions;
using PolisProReminder.Application.Extensions;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Extensions;
using PolisProReminder.Infrastructure.Seeders;
using PolisProReminder.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PolisPro Reminder API");
    });
}

app.UseHttpsRedirection();

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseCors("frontend");
app.UseAuthorization();

app.MapControllers();
    //.RequireAuthorization(new AuthorizeAttribute());

app.Run();
