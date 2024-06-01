using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Infrastructure.Persistance.Configurations;

namespace PolisProReminder.Infrastructure.Persistance;

internal class InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Insurer> Insurers { get; set; } = null!;
    internal DbSet<Policy> Policies { get; set; } = null!;
    internal DbSet<InsuranceCompany> InsuranceCompanies { get; set; } = null!;
    internal DbSet<InsuranceType> InsuranceTypes { get; set; } = null!;
    internal DbSet<User> Users { get; set; } = null!;
    internal DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new InsuranceCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new InsuranceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InsurerConfiguration());
        modelBuilder.ApplyConfiguration(new PolicyConfiguration());
    }
}
