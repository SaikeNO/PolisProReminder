using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new InsuranceCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new InsuranceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BusinessInsurerConfiguration());
        modelBuilder.ApplyConfiguration(new IndividualInsurerConfiguration());
        modelBuilder.ApplyConfiguration(new PolicyConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleBrandConfiguration());
        modelBuilder.ApplyConfiguration(new AttachmentConfiguration());

        modelBuilder.Entity<BaseInsurer>()
            .HasDiscriminator<string>("InsurerType")
            .HasValue<IndividualInsurer>("Individual")
            .HasValue<BusinessInsurer>("Business");
    }
}
