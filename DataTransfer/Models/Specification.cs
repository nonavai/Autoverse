using System;
using System.Collections.Generic;

namespace DataTransfer.Models;

public partial class Specification
{
    public string? ComplectationId { get; set; }

    public string? BackBrake { get; set; }

    public string? Feeding { get; set; }

    public string? HorsePower { get; set; }

    public string? KvtPower { get; set; }

    public string? RpmPower { get; set; }

    public string? EngineType { get; set; }

    public string? Transmission { get; set; }

    public string? Drive { get; set; }

    public string? Volume { get; set; }

    public string? TimeTo100 { get; set; }

    public string? CylindersOrder { get; set; }

    public string? MaxSpeed { get; set; }

    public string? Compression { get; set; }

    public string? CylindersValue { get; set; }

    public string? Diametr { get; set; }

    public string? PistonStroke { get; set; }

    public string? EngineFeeding { get; set; }

    public string? EngineOrder { get; set; }

    public string? GearValue { get; set; }

    public string? Moment { get; set; }

    public string? PetrolType { get; set; }

    public string? Valves { get; set; }

    public string? Weight { get; set; }

    public string? WheelSize { get; set; }

    public string? WheelBase { get; set; }

    public string? FrontWheelBase { get; set; }

    public string? BackWheelBase { get; set; }

    public string? FrontBrake { get; set; }

    public string? FrontSuspension { get; set; }

    public string? BackSuspension { get; set; }

    public string? Height { get; set; }

    public string? Width { get; set; }

    public string? FuelTankCapacity { get; set; }

    public string? Seats { get; set; }

    public string? Length { get; set; }

    public string? EmissionEuroClass { get; set; }

    public string? VolumeLitres { get; set; }

    public string? ConsumptionMixed { get; set; }

    public string? Clearance { get; set; }

    public string? TrunksMinCapacity { get; set; }

    public string? TrunksMaxCapacity { get; set; }

    public string? ConsumptionHiway { get; set; }

    public string? ConsumptionCity { get; set; }

    public string? MomentRpm { get; set; }

    public string? FullWeight { get; set; }

    public string? RangeDistance { get; set; }

    public string? BatteryCapacity { get; set; }

    public string? FuelEmission { get; set; }

    public string? ElectricRange { get; set; }

    public string? ChargeTime { get; set; }

    public string? SafetyRating { get; set; }

    public string? SafetyGrade { get; set; }

    public virtual Modification? Complectation { get; set; }
}
