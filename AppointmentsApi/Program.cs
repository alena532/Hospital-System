using System.Reflection;
using AppointmentsApi.DataAccess;
using AppointmentsApi.Extensions;
using FluentValidation.AspNetCore;


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


services.ConfigureAutoMapper();

services.ConfigureSwagger();

services.ConfigureSqlContext(builder.Configuration);

services.ConfigureFilters();
services.ConfigureRepositories();
services.ConfigureServices();

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