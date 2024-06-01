using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

public class InsuranceTypeConfiguration : IEntityTypeConfiguration<InsuranceType>
{
    public void Configure(EntityTypeBuilder<InsuranceType> builder)
    {
        builder.ToTable("InsuranceTypes");
        builder.Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(30);
    }
}
