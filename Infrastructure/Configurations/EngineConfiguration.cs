using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EngineConfiguration : IEntityTypeConfiguration<Engine>
{
    public void Configure(EntityTypeBuilder<Engine> builder)
    {
        builder.HasKey(engine => engine.ModificationId);
    }
}