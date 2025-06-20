using Domain.Enums;

namespace Domain.Contracts.CleanModels;

public class ModelClean
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public string CyrillicName { get; set; } 
    public Class Class { get; set; } 
    public int YearFrom { get; set; }
    public int YearTo { get; set; } 
    public string MarkId { get; set; }
}