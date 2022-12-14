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
    
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Doctor)
            .WithOne(a => a.Account)
            .HasForeignKey<Doctor>(d => d.AccountId);

        base.OnModelCreating(modelBuilder);
    }
    
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
        { }
    }
    
}