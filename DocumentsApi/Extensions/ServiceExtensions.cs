using DocumentsApi.DataAccess.Repositories.Implementations;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using DocumentsApi.Services.Implementations;
using DocumentsApi.Services.Interfaces;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DocumentsApi.Extensions;

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
    
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IPhotoPatientRepository, PhotoPatientRepository>();
    }
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IPhotosService,PhotosService>();
        
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