using Domain.Enums;

namespace Domain.Contracts.CleanModels;

public class CarConfigurationClean
{
    public string Id { get; set; }
    public int DoorsCount { get; set; } 
    public BodyType BodyType { get; set; } 
    public string? ConfigurationName { get; set; }
    public string GenerationId { get; set; }
}