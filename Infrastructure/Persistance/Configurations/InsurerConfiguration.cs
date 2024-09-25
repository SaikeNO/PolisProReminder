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
            .HasMaxLength(20);

        builder.Property(i => i.Pesel)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(i => i.PostalCode)
            .HasMaxLength(6);

        builder.Property(i => i.City)
            .HasMaxLength(60);

        builder.Property(i => i.Street)
            .HasMaxLength(60);

        builder.HasMany(i => i.Policies)
            .WithOne(p => p.Insurer)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
