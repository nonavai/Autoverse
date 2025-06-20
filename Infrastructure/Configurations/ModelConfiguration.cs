using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(model => model.Id);
        builder.Property(model => model.Id).ValueGeneratedNever();
        
        builder
            .HasOne(model => model.Mark)
            .WithMany(mark => mark.Models)
            .HasForeignKey(model => model.MarkId);
    }
}