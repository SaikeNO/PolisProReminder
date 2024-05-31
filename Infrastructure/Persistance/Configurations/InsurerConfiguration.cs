using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

public class InsurerConfiguration : IEntityTypeConfiguration<Insurer>
{
    public void Configure(EntityTypeBuilder<Insurer> builder)
    {
        builder.ToTable("Insurers");

        builder.Property(i => i.FirstName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(i => i.LastName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(i => i.Pesel)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasMany(i => i.Policies)
            .WithOne(p => p.Insurer)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
