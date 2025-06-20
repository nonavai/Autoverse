using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MobilityConfiguration : IEntityTypeConfiguration<Mobility>
{
    public void Configure(EntityTypeBuilder<Mobility> builder)
    {
        builder.HasKey(mobility => mobility.ModificationId);
    }
}