using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PolisProReminder.Entities.Configurations
{
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
        }
    }
}
