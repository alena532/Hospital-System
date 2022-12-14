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
        services.AddTransient<IDoctorProfilesService,DoctorProfilesService>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IDoctorProfileRepository, DoctorProfileRepository>();
        services.AddTransient<IPatientProfilesService, PatientProfilesService>();
        services.AddTransient<IPatientProfileRepository, PatientProfileRepository>();
        services.AddTransient<IAccountRepository,AccountRepository>();
        services.AddHttpClient<IDoctorProfilesService, DoctorProfilesService>(client =>
            client.BaseAddress = new Uri("http://localhost:5088")
        );
        services.AddHttpClient<IPatientProfilesService, PatientProfilesService>(client =>
            client.BaseAddress = new Uri("http://localhost:5088")
        );
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