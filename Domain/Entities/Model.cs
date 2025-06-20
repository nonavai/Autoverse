using Domain.Enums;

namespace Domain.Entities;

public class Model : BaseEntity
{
    public string Name { get; set; } 
    public string CyrillicName { get; set; } 
    public Class Class { get; set; } 
    public int YearFrom { get; set; }
    public int YearTo { get; set; } 
    public string MarkId { get; set; }
    public Mark Mark { get; set; }
    public IEnumerable<Generation> Generation { get; set; }
}