using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess;
using OfficesApi.DataAccess.Repositories.Implementations;
using OfficesApi.DataAccess.Repositories.Interfaces;
using OfficesApi.Mappers;
using OfficesApi.Services.Implementations;
using OfficesApi.Services.Interfaces;

namespace OfficesApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    
    public static void ConfigureFilters(this IServiceCollection services)
    {
        
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
        services.AddAutoMapper( typeof(OfficesMapper));
    }
    
}