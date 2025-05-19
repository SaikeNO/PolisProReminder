using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Authorization;
using PolisProReminder.Infrastructure.Persistance;
using PolisProReminder.Infrastructure.Repositories;
using PolisProReminder.Infrastructure.Schedulers;
using PolisProReminder.Infrastructure.Security;
using PolisProReminder.Infrastructure.Seeders;

namespace PolisProReminder.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PolisProReminderDB");
        services.AddDbContext<InsuranceDbContext>(options => options.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information));
        services.AddHostedService<ArchivePoliciesService>();

        services.AddIdentityApiEndpoints<User>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.Lockout.AllowedForNewUsers = true;
            options.Stores.ProtectPersonalData = true;
        })
            .AddRoles<UserRole>()
            .AddPersonalDataProtection<LookupProtector, LookupProtectorKeyRing>()
            .AddClaimsPrincipalFactory<InsuranceUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<InsuranceDbContext>();

        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPoliciesRepository, PoliciesRepository>();
        services.AddScoped<IBaseInsurersRepository, BaseInsurersRepository>();
        services.AddScoped<IIndividualInsurersRepository, IndividualInsurersRepository>();
        services.AddScoped<IBusinessInsurersRepository, BusinessInsurersRepository>();
        services.AddScoped<IInsuranceTypesRepository, InsuranceTypesRepository>();
        services.AddScoped<IInsuranceCompaniesRepository, InsuranceCompaniesRepository>();
        services.AddScoped<IVehiclesRepository, VehiclesRepository>();
        services.AddScoped<IVehicleBrandsRepository, VehicleBrandsRepository>();
        services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
        services.AddScoped<ITodoTasksRepository, TodoTasksRepository>();
    }
}
