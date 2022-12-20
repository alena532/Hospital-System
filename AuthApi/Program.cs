using AuthApi.DataAccess;
using AuthApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddHttpContextAccessor();
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
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.Run();