using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Orchestrator.Common.Attributes;
using Orchestrator.Services.Implementations;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",new OpenApiInfo{Title = "MyApi",Version = "v1"});
            c.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2022-01-01")
            });
        }
        );
    }
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IDoctorProfilesService,DoctorProfilesService>();
        services.AddTransient<IPatientProfilesService,PatientProfilesService>();
        services.AddTransient<IReceptionistProfilesService,ReceptionistProfilesService>();
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        var  MyAllowedOrigins = "_myAllowSpecificOrigins";
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowedOrigins,
                policy  =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
    public static void ConfigureFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationModelAttribute>();
    }
    
}