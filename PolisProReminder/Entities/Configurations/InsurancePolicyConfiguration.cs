using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PolisProReminder.Entities.Configurations
{
    public class InsurancePolicyConfiguration : IEntityTypeConfiguration<InsurancePolicy>
    {
        public void Configure(EntityTypeBuilder<InsurancePolicy> builder)
        {
            builder.ToTable("Policies");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
