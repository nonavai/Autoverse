using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EmissionsConfiguration : IEntityTypeConfiguration<Emissions>
{
    public void Configure(EntityTypeBuilder<Emissions> builder)
    {
        builder.HasKey(emissions => emissions.ModificationId);
    }
}