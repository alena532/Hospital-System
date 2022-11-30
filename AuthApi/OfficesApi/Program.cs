using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using OfficesApi.DataAccess.Models;
using OfficesApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddHttpContextAccessor();
services.AddControllers();
services.AddFluentValidation(options =>
{
    
    options.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    
});
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

/*using (var scope = app.Services.CreateScope())
{
    var getServices = scope.ServiceProvider;
    SeedData.Initialize(getServices);
}
*/

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
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();