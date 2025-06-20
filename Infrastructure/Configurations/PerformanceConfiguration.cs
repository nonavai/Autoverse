using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder.HasKey(performance => performance.ModificationId);
        builder.Property(performance => performance.ModificationId).ValueGeneratedNever();

    }
}