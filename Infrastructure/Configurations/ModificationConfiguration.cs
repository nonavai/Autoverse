using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ModificationConfiguration : IEntityTypeConfiguration<Modification>
{
    public void Configure(EntityTypeBuilder<Modification> builder)
    {
        builder.HasKey(modification => modification.Id);
        builder.Property(modification => modification.Id).ValueGeneratedNever();

        
        builder
            .HasOne(modification => modification.CarConfiguration)
            .WithMany(configuration => configuration.Modifications)
            .HasForeignKey(modification => modification.CarConfigurationId);
        
        builder
            .HasOne(modification => modification.Comfort)
            .WithOne(comfort => comfort.Modification)
            .HasForeignKey<Comfort>(comfort  => comfort.ModificationId);
        builder
            .HasOne(modification => modification.Dimension)
            .WithOne(dimension => dimension.Modification)
            .HasForeignKey<Dimension>(dimension  => dimension.ModificationId);
        builder
            .HasOne(modification => modification.Emissions)
            .WithOne(emissions => emissions.Modification)
            .HasForeignKey<Emissions>(emissions  => emissions.ModificationId);
        builder
            .HasOne(modification => modification.Engine)
            .WithOne(engine => engine.Modification)
            .HasForeignKey<Engine>(engine  => engine.ModificationId);
        builder
            .HasOne(modification => modification.Exterior)
            .WithOne(exterior => exterior.Modification)
            .HasForeignKey<Exterior>(exterior  => exterior.ModificationId);
        builder
            .HasOne(modification => modification.Interior)
            .WithOne(interior => interior.Modification)
            .HasForeignKey<Interior>(interior  => interior.ModificationId);
        builder
            .HasOne(modification => modification.Mobility)
            .WithOne(mobility => mobility.Modification)
            .HasForeignKey<Mobility>(mobility  => mobility.ModificationId);
        builder
            .HasOne(modification => modification.Performance)
            .WithOne(performance => performance.Modification)
            .HasForeignKey<Performance>(performance  => performance.ModificationId);
        builder
            .HasOne(modification => modification.Safety)
            .WithOne(safety => safety.Modification)
            .HasForeignKey<Safety>(safety  => safety.ModificationId);
        builder
            .HasOne(modification => modification.Weight)
            .WithOne(weight => weight.Modification)
            .HasForeignKey<Weight>(weight  => weight.ModificationId);
        builder
            .HasOne(modification => modification.Weight)
            .WithOne(weight => weight.Modification)
            .HasForeignKey<Weight>(weight  => weight.ModificationId);
    }
}