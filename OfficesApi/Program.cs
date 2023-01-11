using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using OfficesApi.DataAccess.Models;
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

//TODO: Why?
services.AddHttpContextAccessor();
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
//services.AddValidatorsFromAssemblyContaining<Office>();
services.ConfigureAutoMapper();
//services.ConfigureValidators();

services.ConfigureSwagger();
services.ConfigureOfficesService();
services.ConfigureOfficeRepository();
services.ConfigureOfficeReceptionistsService();
services.ConfigureOfficeReceptionistRepository();
services.ConfigureSqlContext(builder.Configuration);

services.ConfigureFilters();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API"));
}
else
{
    app.UseHsts();
}
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
//TODO: Why?
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