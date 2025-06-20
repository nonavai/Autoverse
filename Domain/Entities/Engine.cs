using Domain.Enums;

namespace Domain.Entities;

public class Engine
{
    public string ModificationId { get; set; }
    public Modification Modification { get; set; }
    public float HorsePower { get; set; }
    public float KvtPower { get; set; }
    public string? RpmPower { get; set; }
    public EngineType EngineType { get; set; }
    public string? EngineFeeding { get; set; }
    public string? EngineOrder { get; set; }
    public string? CylindersOrder { get; set; }
    public int? CylindersValue { get; set; }
    public float? Compression { get; set; }
    public float? Volume { get; set; }
    public float? VolumeLitres { get; set; }
    public string? PetrolType { get; set; }
    public int? Valves { get; set; }
    public int? Moment { get; set; }
    public string? MomentRpm { get; set; }
    public int? GearValue { get; set; }
    public float? PistonStroke { get; set; }
    public float? Diametr { get; set; }
}
