

namespace Domain.Contracts.CleanModels;

public class GenerationClean
{
    public string Id { get; set; }
    public string ModelId { get; set; }
    public string Name { get; set; } 
    public int YearStart { get; set; } 
    public int YearStop { get; set; } 
    public bool IsRestyle { get; set; }
}