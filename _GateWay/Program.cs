using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: true, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddOcelot();
var app = builder.Build();
app.UseRouting();

//ocelot
app.UseHttpsRedirection();
app.UseOcelot().Wait();

app.UseAuthorization();
app.MapControllers();
app.Run();