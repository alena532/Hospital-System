using System.Reflection;
using FluentValidation.AspNetCore;
using MassTransit;
using ProfilesApi.Common.Settings;
using ProfilesApi.Consumers;
using ProfilesApi.Extensions;
using Serilog;
using ServiceExtensions;
using Serilog.Filters;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureCors();

services.AddHttpContextAccessor();
services.AddControllers()
    .AddFluentValidation(options =>
    {
        
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Filter.ByIncludingOnly(Matching.FromSource("ProfilesApi"))
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
services.ConfigureAutoMapper();

services.ConfigureSwagger();

services.ConfigureSqlContext(builder.Configuration);

services.ConfigureValidationModelAttribute();
services.ConfigureFilters();
services.ConfigureRepositories();
services.ConfigureServices();

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

app.UseHttpsRedirection();
app.UseCors(MyAllowedOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
