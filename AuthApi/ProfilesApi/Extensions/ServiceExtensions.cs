using MassTransit.MultiBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProfilesApi.Common.Attributes;
using ProfilesApi.DataAccess;
using ProfilesApi.DataAccess.Repositories.Implementations;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Mappers;
using ProfilesApi.Services.Implementations;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Extensions;

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
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper( typeof(AccountsMapper),typeof(DoctorProfilesMapper));
    }
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IDoctorProfilesService,DoctorProfilesService>();
        services.AddTransient<IMailService, MailService>();
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDoctorProfileRepository, DoctorProfileRepository>();
        services.AddTransient<IAccountRepository,AccountRepository>();
    }
    
    public static void ConfigureFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationModelAttribute>();
    }
    
}