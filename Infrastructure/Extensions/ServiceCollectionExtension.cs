using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;
using PolisProReminder.Infrastructure.Repositories;
using PolisProReminder.Infrastructure.Schedulers;
using PolisProReminder.Infrastructure.Seeders;

namespace PolisProReminder.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PolisProReminderDB");
        services.AddDbContext<InsuranceDbContext>(options => options.UseSqlServer(connectionString));
        services.AddHostedService<ArchivePoliciesService>();

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<InsuranceDbContext>();

        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IPoliciesRepository, PoliciesRepository>();
        services.AddScoped<IInsurersRepository, InsurersRepository>();
        services.AddScoped<IInsuranceTypesRepository, InsuranceTypesRepository>();
        services.AddScoped<IInsuranceCompaniesRepository, InsuranceCompaniesRepository>();
    }
}
