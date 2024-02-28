using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities.Configurations;

namespace PolisProReminder.Entities
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }
        public DbSet<Insurer> Insurers { get; set; } = null!;
        public DbSet<Policy> Policies { get; set; } = null!;
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; } = null!;
        public DbSet<InsuranceType> InsuranceTypes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InsuranceCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InsurerConfiguration());
            modelBuilder.ApplyConfiguration(new PolicyConfiguration());
        }
    }
}
