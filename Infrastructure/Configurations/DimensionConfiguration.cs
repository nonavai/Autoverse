using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class DimensionConfiguration : IEntityTypeConfiguration<Dimension>
{
    public void Configure(EntityTypeBuilder<Dimension> builder)
    {
        builder.HasKey(reportToFile => reportToFile.ModificationId);
    }
}