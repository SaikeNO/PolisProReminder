using PolisProReminder.Data;
using PolisProReminder.Entities;
using PolisProReminder.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<InsuranceDbContext>();
builder.Services.AddScoped<IInsurancePolicySerivce, InsurancePolicySerivce>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskList API");
});

app.CreateDbIfNotExists();
app.UseAuthorization();

app.MapControllers();

app.Run();
