using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities.Configurations;

namespace PolisProReminder.Entities
{
    public class InsuranceDbContext : DbContext
    {
        public DbSet<Insurer> Insurers { get; set; } = null!;
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; } = null!;
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; } = null!;
        public DbSet<InsuranceType> InsuranceTypes { get; set; } = null!;

        private readonly string _connectionString =
           "Server=(localdb)\\mssqllocaldb;Database=PolisProReminder;Trusted_Connection=True";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InsuranceCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new InsuranceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InsurerConfiguration());
            modelBuilder.ApplyConfiguration(new InsurancePolicyConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
