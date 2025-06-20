using Domain.Enums;

namespace Domain.Entities;

public class CarConfiguration : BaseEntity
{
    public int DoorsCount { get; set; } 
    public BodyType BodyType { get; set; } 
    public string? ConfigurationName { get; set; }
    public string GenerationId { get; set; }
    public Generation Generation { get; set; }
    public IEnumerable<Modification> Modifications { get; set; }
}