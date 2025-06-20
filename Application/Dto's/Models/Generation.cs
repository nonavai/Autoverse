using Domain.Entities;

namespace Application.Dto_s.Models;

public class Generation
{
    public string Id { get; set; }
    public string ModelId { get; set; }
    public string Name { get; set; } 
    public int YearStart { get; set; } 
    public int YearStop { get; set; } 
    public bool IsRestyle { get; set; }
    public IEnumerable<CarConfiguration> CarConfigurations { get; set; }
}