namespace Domain.Entities;

public class Emissions
{
    public string ModificationId { get; set; }
    public Modification Modification { get; set; }
    public int? FuelEmission { get; set; }
    public string? EmissionEuroClass { get; set; }
    public int? ElectricRange { get; set; }
    public float? ChargeTime { get; set; }
}
