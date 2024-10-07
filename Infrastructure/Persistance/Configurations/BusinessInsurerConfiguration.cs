using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

internal class BusinessInsurerConfiguration : BaseInsurerConfiguration<BusinessInsurer>
{
    public override void Configure(EntityTypeBuilder<BusinessInsurer> builder)
    {
        base.Configure(builder);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(i => i.Nip)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(i => i.Regon)
            .IsRequired()
            .HasMaxLength(9);
    }
}
