namespace Domain.Entities;

public class Performance
{
    public string ModificationId { get; set; }
    public Modification Modification { get; set; }
    public float? TimeTo100 { get; set; }
    public float? MaxSpeed { get; set; }
    public decimal? ConsumptionMixed { get; set; }
    public decimal? ConsumptionHiway { get; set; }
    public decimal? ConsumptionCity { get; set; }
    public decimal? RangeDistance { get; set; }
    public decimal? ElectricRange { get; set; }
}