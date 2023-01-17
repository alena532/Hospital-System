using Microsoft.EntityFrameworkCore;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.DataAccess;

public static class SeedData
{
    public static async Task Initialize(IServiceScope scope)
    {
        using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
        {
            if (context.ServiceCategories.Any()) return;
            
            context.ServiceCategories.AddRange(
                new ServiceCategory(){CategoryName = "Analyses",TimeSlotSize = 1},
                new ServiceCategory(){CategoryName = "Diagnostic",TimeSlotSize = 3},
                new ServiceCategory(){CategoryName = "Consultation",TimeSlotSize = 2}
            );

            context.SaveChanges();
        }
    }
}