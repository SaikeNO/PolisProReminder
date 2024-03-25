using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PolisProReminder.Entities.Configurations;

public class InsuranceCompanyConfiguration : IEntityTypeConfiguration<InsuranceCompany>
{
    public void Configure(EntityTypeBuilder<InsuranceCompany> builder)
    {
        builder.ToTable("InsuranceCompanies");
        builder.Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(60);
    }
}
