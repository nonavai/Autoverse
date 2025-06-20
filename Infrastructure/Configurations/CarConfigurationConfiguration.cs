using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CarConfigurationConfiguration: IEntityTypeConfiguration<CarConfiguration>
{
    public void Configure(EntityTypeBuilder<CarConfiguration> builder)
    {
        builder.HasKey(configuration => configuration.Id);
        builder.Property(configuration => configuration.Id).ValueGeneratedNever();

        
        builder
            .HasOne(configuration => configuration.Generation)
            .WithMany(generation => generation.CarConfigurations)
            .HasForeignKey(configuration => configuration.GenerationId);
    }
}