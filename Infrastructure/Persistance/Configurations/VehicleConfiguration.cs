using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(v => v.VIN)
            .HasMaxLength(256);

        builder.Property(v => v.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(256);

    }
}
