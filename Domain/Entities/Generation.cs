namespace Domain.Entities;

public class Generation : BaseEntity
{
    public string ModelId { get; set; }
    public Model Model { get; set; }
    public string Name { get; set; } 
    public int YearStart { get; set; } 
    public int YearStop { get; set; } 
    public bool IsRestyle { get; set; }
    public IEnumerable<CarConfiguration> CarConfigurations { get; set; }
}