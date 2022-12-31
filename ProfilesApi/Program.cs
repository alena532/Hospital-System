using System.Reflection;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ProfilesApi.Common.Settings;
using ProfilesApi.Consumers;
using ProfilesApi.Extensions;
using ProfilesApi.Services.Implementations;
using Serilog;
using Serilog.Filters;
var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureCors();

services.AddControllers()
    .AddFluentValidation(options =>
    {
        
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });


services.AddHttpContextAccessor();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Filter.ByIncludingOnly(Matching.FromSource("ProfilesApi"))
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
services.ConfigureAutoMapper();

services.ConfigureSwagger();

services.ConfigureServices();
services.ConfigureFilters();

services.ConfigureSqlContext(builder.Configuration);
services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

services.AddMassTransit(x =>
    {
        x.AddConsumer<OfficeUpdatedConsumer>();
        x.SetKebabCaseEndpointNameFormatter();
        x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
            
    }

);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
else
{
    app.UseHsts();
}
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowedOrigins);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();