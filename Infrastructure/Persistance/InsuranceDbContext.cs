﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Persistance.Configurations;

namespace PolisProReminder.Infrastructure.Persistance;

internal class InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : IdentityDbContext<User, UserRole, Guid>(options)
{
    internal DbSet<IndividualInsurer> IndividualInsurers { get; set; } = null!;
    internal DbSet<BusinessInsurer> BusinessInsurers { get; set; } = null!;
    internal DbSet<Policy> Policies { get; set; } = null!;
    internal DbSet<InsuranceCompany> InsuranceCompanies { get; set; } = null!;
    internal DbSet<InsuranceType> InsuranceTypes { get; set; } = null!;
    internal DbSet<Vehicle> Vehicles { get; set; } = null!;
    internal DbSet<VehicleBrand> VehicleBrands { get; set; } = null!;
    internal DbSet<Attachment> Attachments { get; set; } = null!;
    internal DbSet<TodoTask> TodoTasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new InsuranceCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new InsuranceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BusinessInsurerConfiguration());
        modelBuilder.ApplyConfiguration(new IndividualInsurerConfiguration());
        modelBuilder.ApplyConfiguration(new PolicyConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleBrandConfiguration());
        modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new TodoTaskConfiguration());

        modelBuilder.Entity<BaseInsurer>()
            .HasDiscriminator<string>("InsurerType")
            .HasValue<IndividualInsurer>("Individual")
            .HasValue<BusinessInsurer>("Business");

        modelBuilder.Entity<BaseInsurer>()
            .HasMany(i => i.Policies)
            .WithMany(p => p.Insurers)
            .UsingEntity<Dictionary<string, object>>(
                "InsurerPolicies",
                j => j.HasOne<Policy>().WithMany().HasForeignKey("PolicyId"),
                j => j.HasOne<BaseInsurer>().WithMany().HasForeignKey("InsurerId"),
                j =>
                {
                    j.HasKey("PolicyId", "InsurerId");
                });

        modelBuilder.Entity<BaseInsurer>()
           .HasMany(i => i.Vehicles)
           .WithMany(p => p.Insurers)
           .UsingEntity<Dictionary<string, object>>(
               "InsurerVehicles",
               j => j.HasOne<Vehicle>().WithMany().HasForeignKey("VehicleId"),
               j => j.HasOne<BaseInsurer>().WithMany().HasForeignKey("InsurerId"),
               j =>
               {
                   j.HasKey("VehicleId", "InsurerId");
               });
    }
}
