using MailKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ServicesApi.Common.Attributes;
using ServicesApi.DataAccess;
using ServicesApi.DataAccess.Repositories.Implementations;
using ServicesApi.DataAccess.Repositories.Interfaces;
using ServicesApi.Mappers;
using ServicesApi.Services.Implementations;
using ServicesApi.Services.Interfaces;

namespace ServicesApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",new OpenApiInfo{Title = "MyApi",Version = "v1"});
        });
    }
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServicesMapper));
        services.AddAutoMapper(typeof(SpecializationsMapper));
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
         services.AddTransient<IServicesService,ServicesService>();
         services.AddTransient<ISpecializationsService,SpecializationsService>();
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