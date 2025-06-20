namespace Domain.Entities;

public class Weight
{
    public string ModificationId { get; set; }
    public Modification Modification { get; set; }
    public int? BaseWeight { get; set; }
    public int? FullWeight { get; set; }
    public int? FuelTankCapacity { get; set; }
    public float? BatteryCapacity { get; set; }
}