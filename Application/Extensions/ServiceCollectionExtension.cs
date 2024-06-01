using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PolisProReminder.Application.InsuranceCompanies;
using PolisProReminder.Application.InsuranceTypes;
using PolisProReminder.Application.Insurers;
using PolisProReminder.Application.Policies;

namespace PolisProReminder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
        
        services.AddScoped<IPoliciesService, PoliciesService>();
        services.AddScoped<IInsurersService, InsurersService>();
        services.AddScoped<IInsuranceCompaniesService, InsuranceCompaniesService>();
        services.AddScoped<IInsuranceTypesService, InsuranceTypesService>();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
