using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SafetyConfiguration : IEntityTypeConfiguration<Safety>
{
    public void Configure(EntityTypeBuilder<Safety> builder)
    {
        builder.HasKey(safety => safety.ModificationId);
    }
}