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
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AccountsMapper),typeof(DoctorProfilesMapper));
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDoctorProfileRepository, DoctorProfileRepository>();
        services.AddScoped<IPatientProfileRepository, PatientProfileRepository>();
        services.AddScoped<IReceptionistProfileRepository, ReceptionistProfileRepository>();
        services.AddScoped<IAccountRepository,AccountRepository>();
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IDoctorProfilesService,DoctorProfilesService>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IPatientProfilesService, PatientProfilesService>();
        services.AddTransient<IReceptionistProfilesService, ReceptionistProfilesService>();
        services.AddTransient<IAccountsService, AccountsService>();
        
        services.AddHttpClient<IDoctorProfilesService, DoctorProfilesService>(client =>
            client.BaseAddress = new Uri("http://localhost:5088")
        );
        services.AddHttpClient<IPatientProfilesService, PatientProfilesService>(client =>
            client.BaseAddress = new Uri("http://localhost:5088")
        );
    }
    
    public static void ConfigureFilters(this IServiceCollection services)
    {
       
    }
    
}