using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

public abstract class BaseInsurerConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : Insurer
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.Property(i => i.Email)
            .HasMaxLength(60);

        builder.Property(i => i.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(i => i.PostalCode)
            .HasMaxLength(6);

        builder.Property(i => i.City)
            .HasMaxLength(60);

        builder.Property(i => i.Street)
            .HasMaxLength(60);
    }
}
