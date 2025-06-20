using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ComfortConfiguration : IEntityTypeConfiguration<Comfort>
{
    public void Configure(EntityTypeBuilder<Comfort> builder)
    {
        builder.HasKey(reportToFile => reportToFile.ModificationId);
    }
}