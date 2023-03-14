using AuthApi.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.BackgroundTasks;

public class RolesBackgroundTask : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RolesBackgroundTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    { 
        IServiceScope scope = _serviceProvider.CreateScope();
        using (var context = new AppDbContext(
                   scope.ServiceProvider.GetRequiredService<
                       DbContextOptions<AppDbContext>>()))
        {
            
            if (context.Roles.Any()) return;
            
            context.Roles.AddRange(
                new Role(){Name = "Doctor"},
                new Role(){Name = "Patient"},
                new Role(){Name = "Receptionist"}
            );

            context.SaveChanges();
        }
    }
}