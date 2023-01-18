using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Filters;
using ServicesApi.DataAccess;
using ServicesApi.Extensions;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureCors();

services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});

services.AddControllers();
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder
    .UseLazyLoadingProxies()
    .UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        optionsAction =>
        {
            optionsAction.CommandTimeout((int)TimeSpan.FromSeconds(120).TotalSeconds);
            optionsAction.EnableRetryOnFailure();
        }
    );
services.AddScoped<AppDbContext>(instance => new AppDbContext(optionsBuilder.Options));
//services.ConfigureSqlContext(builder.Configuration);

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


services.ConfigureFilters();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    SeedData.Initialize(scope);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API"));
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseCors(MyAllowedOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
