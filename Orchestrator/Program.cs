using Orchestrator.Extensions;
using ServiceExtensions;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureCors();

services.AddControllers();

services.ConfigureSwagger();
services.ConfigureValidationModelAttribute();
services.ConfigureFilters();
services.ConfigureServices();
services.ConfigureAutoMapper();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

//app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseCors(MyAllowedOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
