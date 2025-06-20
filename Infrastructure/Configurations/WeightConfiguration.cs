using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class WeightConfiguration : IEntityTypeConfiguration<Weight>
{
    public void Configure(EntityTypeBuilder<Weight> builder)
    {
        builder.HasKey(weight => weight.ModificationId);
    }
}