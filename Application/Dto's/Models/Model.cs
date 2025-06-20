using Domain.Enums;

namespace Application.Dto_s.Models;

public class Model
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public string CyrillicName { get; set; } 
    public Class Class { get; set; } 
    public int YearFrom { get; set; }
    public int YearTo { get; set; } 
    public string MarkId { get; set; }
    public IEnumerable<Generation> Generation { get; set; }
}