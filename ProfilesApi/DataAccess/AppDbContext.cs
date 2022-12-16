using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.DataAccess;

public class AppDbContext:DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Doctor)
            .WithOne(a => a.Account)
            .HasForeignKey<Doctor>(d => d.AccountId);

        base.OnModelCreating(modelBuilder);
    }
    
   
    
}