using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ExteriorConfiguration : IEntityTypeConfiguration<Exterior>
{
    public void Configure(EntityTypeBuilder<Exterior> builder)
    {
        builder.HasKey(exterior => exterior.ModificationId);
    }
}