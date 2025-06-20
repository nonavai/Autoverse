namespace Application.Dto_s.Models;

public class Emissions
{
    public string ModificationId { get; set; }
    public int? FuelEmission { get; set; }
    public string? EmissionEuroClass { get; set; }
    public int? ElectricRange { get; set; }
    public float? ChargeTime { get; set; }
}
