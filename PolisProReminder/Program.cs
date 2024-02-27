using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using PolisProReminder.Data;
using PolisProReminder.Entities;
using PolisProReminder.Middlewares;
using PolisProReminder.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<InsuranceDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IInsurerService, InsurerService>();
builder.Services.AddScoped<IInsuranceCompanyService, InsuranceCompanyService>();
builder.Services.AddScoped<IInsuranceTypeService, InsuranceTypeService>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PolisPro Reminder API", Version = "v1" });
});
builder.Logging.ClearProviders();
builder.Host.UseNLog();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PolisPro Reminder API");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.CreateDbIfNotExists();
app.UseAuthorization();

app.MapControllers();

app.Run();
