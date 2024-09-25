using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

internal class IndividualInsurerConfiguration : BaseInsurerConfiguration<IndividualInsurer>
{
    public override void Configure(EntityTypeBuilder<IndividualInsurer> builder)
    {
        base.Configure(builder); 

        builder.Property(i => i.FirstName)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(i => i.LastName)
            .HasMaxLength(60);

        builder.Property(i => i.Pesel)
            .IsRequired()
            .HasMaxLength(11);
    }
}
