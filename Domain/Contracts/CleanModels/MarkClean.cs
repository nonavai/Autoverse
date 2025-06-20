using Domain.Enums;

namespace Domain.Contracts.CleanModels;

public class MarkClean
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CyrillicName { get; set; }
    public int Popular { get; set; }
    public Country Country { get; set; }
}