using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.ToTable("Policies");

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(p => p.PolicyNumber)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(p => p.InsuranceCompany)
            .WithMany(c => c.Policies)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
