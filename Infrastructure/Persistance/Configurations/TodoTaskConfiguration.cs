using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Infrastructure.Persistance.Configurations;

public class TodoTaskConfiguration : IEntityTypeConfiguration<TodoTask>
{
    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder.ToTable("TodoTasks");

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(250);
    }
}
