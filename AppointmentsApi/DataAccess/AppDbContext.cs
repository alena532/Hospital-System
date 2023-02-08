using AppointmentsApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApi.DataAccess;


public class AppDbContext:DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<DoctorAppointment> DoctorAppointments { get; set; }
    public DbSet<PatientAppointment> PatientAppointments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.DoctorAppointment)
            .WithMany(a => a.Appointments)
            .HasForeignKey(d => d.DoctorAppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.PatientAppointment)
            .WithMany(a => a.Appointments)
            .HasForeignKey(d => d.PatientAppointmentId)
            .OnDelete(DeleteBehavior.Cascade); 
            

        base.OnModelCreating(modelBuilder);
    }
    
   
    
}