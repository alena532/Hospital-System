using _GateWay.Extensions;
using Ocelot.Middleware;
var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Configuration.AddJsonFile("ocelot.json", optional: true, reloadOnChange: true);
services.ConfigureCors();
services.AddControllers();
services.ConfigureSwagger();
services.ConfigureJWT(builder.Configuration);
services.ConfigureServices();
var app = builder.Build();
//app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API"));
}
app.UseRouting();
//app.UseHttpsRedirection();
app.UseCors(MyAllowedOrigins);
app.UseAuthentication();

app.UseOcelot().Wait();
app.Run();