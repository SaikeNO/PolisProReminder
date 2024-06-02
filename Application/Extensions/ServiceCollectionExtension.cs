using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PolisProReminder.Application.InsuranceCompanies;
using PolisProReminder.Application.InsuranceTypes;
using PolisProReminder.Application.Users;

namespace PolisProReminder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
        
        services.AddScoped<IInsuranceCompaniesService, InsuranceCompaniesService>();
        services.AddScoped<IInsuranceTypesService, InsuranceTypesService>();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
