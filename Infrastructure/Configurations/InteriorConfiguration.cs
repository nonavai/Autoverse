using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class InteriorConfiguration : IEntityTypeConfiguration<Interior>
{
    public void Configure(EntityTypeBuilder<Interior> builder)
    {
        builder.HasKey(interior => interior.ModificationId);
    }
}