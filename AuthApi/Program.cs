
using AuthApi.DataAccess;
using AuthApi.Extensions;
using Microsoft.AspNet.Identity;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.ConfigureCors();
services.AddControllers();

services.ConfigureSwagger();

services.ConfigureSqlContext(builder.Configuration);

services.ConfigureIdentity();
services.ConfigureJWT(builder.Configuration);
services.ConfigureFilters();
services.ConfigureServices();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var getServices = scope.ServiceProvider;
    SeedData.Initialize(getServices);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API"));
}

app.UseHttpsRedirection();
app.UseCors(MyAllowedOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();