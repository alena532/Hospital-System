using FluentValidation.AspNetCore;
using MassTransit;
using OfficesApi.Extensions;
using Serilog;
using Serilog.Filters;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureCors();

services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});

services.AddControllers();
services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Filter.ByIncludingOnly(Matching.FromSource("OfficesApi"))
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
services.ConfigureAutoMapper();

services.ConfigureSwagger();
services.ConfigureServices();
services.ConfigureRepositories();

services.ConfigureSqlContext(builder.Configuration);

services.ConfigureFilters();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API"));
}
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseCors(MyAllowedOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

