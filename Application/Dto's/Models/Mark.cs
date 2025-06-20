using Domain.Enums;

namespace Application.Dto_s.Models;

public class Mark
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CyrillicName { get; set; }
    public int Popular { get; set; }
    public Country Country { get; set; }
    public IEnumerable<Model> Models { get; set; }
}