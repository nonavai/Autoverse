using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Context;

public class AutoVerseContext : DbContext
{
    public DbSet<Comfort> Comforts { get; set; }
    public DbSet<CarConfiguration> CarConfigurations { get; set; }
    public DbSet<Dimension> Dimensions { get; set; }
    public DbSet<Emissions> Emissions { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Exterior> Exteriors { get; set; }
    public DbSet<Generation> Generations { get; set; }
    public DbSet<Interior> Interiors { get; set; }
    public DbSet<Mark> Marks { get; set; }
    public DbSet<Mobility> Mobilities { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Modification> Modifications { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<Safety> Safeties { get; set; }
    public DbSet<Weight> Weights { get; set; }
    
    static readonly string connectionString = "User ID=postgres;Password=123;Host=localhost;Port=5432;Database=AutoVerseDb;Pooling=true;Connection Lifetime=0;Include Error Detail=true;";
    
    public AutoVerseContext(DbContextOptions<AutoVerseContext> options, IConfiguration configuration) : base(options)
    {
    }
    /*public AutoVerseContext()
    {
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarkConfiguration).Assembly);
        modelBuilder.HasDefaultSchema("public");
    }
}