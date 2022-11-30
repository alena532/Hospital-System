using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.DataAccess;

public class AppDbContext:DbContext
{
    public DbSet<Office> Offices { get; set; }
    public DbSet<OfficeReceptionist> OfficeReceptionists { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OfficeReceptionist>(entity =>
        {
            entity.HasOne(e => e.Office)
                .WithMany(e => e.OfficeReceptionists)
                .HasForeignKey(e => e.OfficeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
    
}