using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.DataAccess;

public class AppDbContext:DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Receptionist> Receptionists { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Doctor)
            .WithOne(a => a.Account)
            .HasForeignKey<Doctor>(d => d.AccountId);
        
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Receptionist)
            .WithOne(a => a.Account)
            .HasForeignKey<Receptionist>(d => d.AccountId);
            

        base.OnModelCreating(modelBuilder);
    }
    
   
    
}