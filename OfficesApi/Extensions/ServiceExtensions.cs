using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfficesApi.Common.Attributes;
using OfficesApi.DataAccess;
using OfficesApi.DataAccess.Repositories.Implementations;
using OfficesApi.DataAccess.Repositories.Interfaces;
using OfficesApi.Mappers;
using OfficesApi.Services.Implementations;
using OfficesApi.Services.Interfaces;

namespace OfficesApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            };

            c.AddSecurityDefinition("jwt_auth", securityDefinition);
    
            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
            {
                {securityScheme, new string[] { }},
            };
            c.AddSecurityRequirement(securityRequirements);

        });
    }
    
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    
    public static void ConfigureFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationModelAttribute>();
    }
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IOfficesService,OfficesService>();
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOfficeRepository,OfficeRepository>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper( typeof(OfficesMapper),typeof(OfficeReceptionistsMapper));
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
}