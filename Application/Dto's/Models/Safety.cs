namespace Application.Dto_s.Models;

public class Safety
{
    public string ModificationId { get; set; }
    public string? SafetyRating { get; set; }
    public int? SafetyGrade { get; set; }
    public bool ABS { get; set; }
    public bool CurtainAirbags { get; set; }
    public bool DriverAirbag { get; set; }
    public bool PassengerAirbag { get; set; }
    public bool RearSideAirbag { get; set; }
    public bool SideAirbag { get; set; }
    public bool ASR { get; set; }
    public bool BAS { get; set; }
    public bool BlindSpotMonitor { get; set; }
    public bool CollisionPreventionAssist { get; set; }
    public bool DownhillAssist { get; set; }
    public bool DrowsyDriverAlertSystem { get; set; }
    public bool ESP { get; set; }
    public bool FeedbackAlarm { get; set; }
    public bool GLONASS { get; set; }
    public bool HillControl { get; set; }
    public bool ISOFIX { get; set; }
    public bool FrontISOFIX { get; set; }
    public bool KneeAirbag { get; set; }
    public bool LaminatedSafetyGlass { get; set; }
    public bool LaneKeepingAssist { get; set; }
    public bool NightVision { get; set; }
    public bool RearDoorPowerChildLocks { get; set; }
    public bool TrafficSignRecognition { get; set; }
    public bool TyrePressureMonitoring { get; set; }
    public bool VSM { get; set; }
    public bool Alarm { get; set; }
    public bool Immobiliser { get; set; }
    public bool Lock { get; set; }
    public bool VolumeSensor { get; set; }
}