using Domain.Enums;

namespace Domain.Entities;

public class Mark: BaseEntity
{
    public string Name { get; set; }
    public string CyrillicName { get; set; }
    public int Popular { get; set; }
    public Country Country { get; set; }
    public IEnumerable<Model> Models { get; set; }
}