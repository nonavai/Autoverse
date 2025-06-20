using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class GenerationConfiguration : IEntityTypeConfiguration<Generation>
{
    public void Configure(EntityTypeBuilder<Generation> builder)
    {
        builder.HasKey(generation => generation.Id);
        
        builder
            .HasOne(generation => generation.Model)
            .WithMany(model => model.Generation)
            .HasForeignKey(generation => generation.ModelId);
    }
}