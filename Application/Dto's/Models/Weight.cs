namespace Application.Dto_s.Models;

public class Weight
{
    public string ModificationId { get; set; }
    public int? BaseWeight { get; set; }
    public int? FullWeight { get; set; }
    public int? FuelTankCapacity { get; set; }
    public float? BatteryCapacity { get; set; }
}