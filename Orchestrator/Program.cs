using System.Reflection;
using Orchestrator.Extensions;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureCors();

//TODO: Why?
services.AddHttpContextAccessor();
services.AddControllers();

services.ConfigureSwagger();
services.ConfigureFilters();
services.ConfigureServices();

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