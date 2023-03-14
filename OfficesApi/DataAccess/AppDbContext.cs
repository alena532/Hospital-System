using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.DataAccess;

public class AppDbContext:DbContext
{
    public DbSet<Office> Offices { get; set; }
    

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
    
}