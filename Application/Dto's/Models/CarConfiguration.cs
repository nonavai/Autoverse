using Application.Dto_s.Models.HelpModels;
using Domain.Enums;

namespace Application.Dto_s.Models;

public class CarConfiguration
{
    public string Id { get; set; }
    public int DoorsCount { get; set; } 
    public BodyType BodyType { get; set; } 
    public string? ConfigurationName { get; set; }
    public string GenerationId { get; set; }
    public IEnumerable<Dto_s.Models.Modification> Modifications { get; set; }
}