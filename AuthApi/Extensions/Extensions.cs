using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthApi.Common.Attributes;
using ServiceExtensions.ConfigurationOptions;
using AuthApi.DataAccess;
using AuthApi.DataAccess.Repositories.Implementations;
using AuthApi.DataAccess.Repositories.Interfaces;
using AuthApi.Services.Implementations;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace AuthApi.Extensions;

public static class Extensions
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
        //services.AddScoped<IUsersService,IUsersService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUsersService,UsersService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthValidatorService,AuthValidatorService>();
        //services.AddScoped<IUsersService,IUsersService>();
       
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services
            .AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

    }
    
}