namespace Domain.Entities;

public class Exterior
{
    public string ModificationId { get; set; }
    public Modification Modification { get; set; }
    public bool AdaptiveLight { get; set; }
    public bool AutomaticLightingControl { get; set; }
    public bool DaytimeRunningLights { get; set; }
    public bool HeatedWashSystem { get; set; }
    public bool HighBeamAssist { get; set; }
    public bool LaserLights { get; set; }
    public bool LEDLights { get; set; }
    public bool LightCleaner { get; set; }
    public bool LightSensor { get; set; }
    public bool HeatedMirrors { get; set; }
    public bool FrontFogLights { get; set; }
    public bool RainSensor { get; set; }
    public bool HeatedWindshieldCleaner { get; set; }
    public bool HeatedWindscreen { get; set; }
    public bool XenonLights { get; set; }
    public bool DoubleColoredBody { get; set; }
    public bool MetallicColored { get; set; }
    public bool RoofRails { get; set; }
    public bool SteelWheels { get; set; }
    public bool DoorSillPanel { get; set; }
    public bool BodyKit { get; set; }
    public bool Mouldings { get; set; }
}