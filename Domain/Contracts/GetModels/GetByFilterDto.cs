using Domain.Contracts.CleanModels;
using Domain.Entities;
using Domain.Enums;
using DriveType = Domain.Enums.DriveType;

namespace Domain.Contracts.GetModels;

public class GetByFilterDto
{
    // Pagination parameters
    public int PageSize;
    public int PageNumber;

    // Identification filters (Mark & Model entities)
    public IEnumerable<string>? MarkIds = null;
    public IEnumerable<string>? ModelIds = null;

    // Configuration filters (CarConfiguration entity)
    public BodyType? BodyType;
    public string? Seats;
    public int? TrunksMinCapacity;
    public int? TrunksMaxCapacity;

    // Financial & production year filters (Model / Generation entities)
    public int? MinCost;
    public int? MaxCost;
    public int? MinYear;
    public int? MaxYear;

    // Power-train filters (Engine, Transmission, Performance & Drive related entities)
    public EngineType? EngineType;
    public TransmissionType? TransmissionType;
    public DriveType? DriveType;
    public float? VolumeLiters;
    public int? MinHorsePower;
    public int? MaxHorsePower;
    public float? MinConsumptionMixed;
    public float? MaxConsumptionMixed;

    // Dimension filters (Dimension entity)
    public float? MinHeight;
    public float? MaxHeight;
    public float? MinWidth;
    public float? MaxWidth;
    public float? MinLength;
    public float? MaxLength;
    public float? FrontWheelBase;
    public float? BackWheelBase;
    public string? Clearance;

    // Comfort options (Comfort entity)
    public bool? Camera360 { get; set; }
    public bool? AshtrayAndCigaretteLighter { get; set; }
    public bool? AutoCruise { get; set; }
    public bool? AutoMirrors { get; set; }
    public bool? AutoPark { get; set; }
    public ClimateControl? ClimateControl { get; set; } 
    public bool? Computer { get; set; }
    public bool? Condition { get; set; }
    public bool? CoolingBox { get; set; }
    public bool? CruiseControl { get; set; }
    public bool? DriveModeSystem { get; set; }
    public bool? EasyTrunkOpening { get; set; }
    public bool? ElectroMirrors { get; set; }
    public bool? ElectroTrunk { get; set; }
    public bool? ElectroWindowBack { get; set; }
    public bool? ElectroWindowFront { get; set; }
    public bool? ElectronicGagePanel { get; set; }
    public bool? FrontCamera { get; set; }
    public bool? KeylessEntry { get; set; }
    public bool? MultiFunctionSteeringWheel { get; set; }
    public bool? ParkAssistFront { get; set; }
    public bool? ParkAssistRear { get; set; }
    public bool? PowerLatchingDoors { get; set; }
    public bool? ProgrammedBlockHeater { get; set; }
    public bool? ProjectionDisplay { get; set; }
    public bool? RearCamera { get; set; }
    public bool? RemoteEngineStart { get; set; }
    public bool? Servo { get; set; }
    public bool? StartButton { get; set; }
    public bool? StartStopFunction { get; set; }
    public bool? SteeringWheelGearShiftPaddles { get; set; }
    public string? WheelHightConfiguration { get; set; }
    public string? WheelDistanceConfiguration { get; set; }
    public bool? WheelMemory { get; set; }
    public bool? WheelPower { get; set; }
    public bool? Socket12V { get; set; }
    public bool? Socket220V { get; set; }
    public bool? AndroidAuto { get; set; }
    public bool? AudioPreparation { get; set; }
    public bool? CdAudioSystem { get; set; }
    public bool? TVAudioSystem { get; set; }
    public bool? PremiumAudio { get; set; }
    public bool? Multimedia { get; set; }
    public bool? AUX { get; set; }
    public bool? AppleCarplay { get; set; }
    public bool? Bluetooth { get; set; }
    public bool? Navigation { get; set; }
    public bool? USB { get; set; }
    public bool? VoiceRecognition { get; set; }
    public bool? WirelessCharger { get; set; }
    public bool? YaAuto { get; set; }
    public bool? HeatedSteeringWheel { get; set; }
    public bool? DriverSeatSupport { get; set; }
    public bool? SeatUpDown { get; set; }
    public bool? ElectroRegulatingSeat { get; set; }
    public bool? SeatSupport { get; set; }
    public bool? SeatsHeat { get; set; }
    public bool? SeatsHeatVent { get; set; }
    public bool? MassageSeats { get; set; }
}