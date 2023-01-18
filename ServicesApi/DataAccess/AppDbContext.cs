using Microsoft.EntityFrameworkCore;
using ServicesApi.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;


namespace ServicesApi.DataAccess;

public class AppDbContext:DbContext
{
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Specialization> Specializations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>()
            .HasOne(a => a.Specialization)
            .WithMany(a => a.Services)
            .HasForeignKey(d => d.SpecializationId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        modelBuilder.Entity<Service>()
            .HasOne(a => a.ServiceCategory)
            .WithMany(a => a.Services)
            .HasForeignKey(d => d.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
    
   
    
}