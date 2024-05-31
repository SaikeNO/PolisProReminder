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
        services.AddScoped<IPolicyService, PoliciesService>();
        services.AddScoped<IInsurerService, InsurersService>();
        services.AddScoped<IInsuranceCompanyService, InsuranceCompaniesService>();
        services.AddScoped<IInsuranceTypeService, InsuranceTypesService>();

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);
    }
}
