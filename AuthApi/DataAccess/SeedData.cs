
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.DataAccess;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<AppDbContext>>()))
        {
            //serviceProvider.GetRequiredService<RoleManager<Role>>()
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