using Microsoft.OpenApi.Models;
using PolisProReminder.Middlewares;

namespace PolisProReminder.API.Extensions;

public static class WebApplicationBuilderExtenstions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PolisPro Reminder API", Version = "v1" });
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.Http
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                    },
                    []
                }
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options => options.AddPolicy("frontend",
            policy =>
            {
                policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            }
        ));

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
    }
}
