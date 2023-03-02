using AppointmentsApi.DataAccess;
using AppointmentsApi.DataAccess.Repositories.Implementations;
using AppointmentsApi.DataAccess.Repositories.Interfaces;
using AppointmentsApi.Mappers;
using AppointmentsApi.Services;
using AppointmentsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace AppointmentsApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",new OpenApiInfo{Title = "MyApi",Version = "v1"});
            c.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            });
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
        services.AddAutoMapper(typeof(AppointmentsMapper),typeof(DoctorAppointmentsMapper),typeof(PatientAppointmentsMapper));
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDoctorAppointmentRepository,DoctorAppointmentRepository>();
        services.AddScoped<IPatientAppointmentRepository,PatientAppointmentRepository>();
        
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IAppointmentsService,AppointmentsService>();
        
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
        //services.AddScoped<ValidationModelAttribute>();
    }
    
}