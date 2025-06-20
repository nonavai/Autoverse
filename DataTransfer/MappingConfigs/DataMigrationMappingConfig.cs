using System.Globalization;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using DataTransfer.MappingConfigs.MappingHelpers;
using Modification = DataTransfer.Models.Modification;

namespace DataTransfer.MappingConfigs;

public class DataMigrationMappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig<Models.Mark, Domain.Entities.Mark>.NewConfig()
            .Map(dest => dest.Id, src => src.Id) 
            .Map(dest => dest.Popular, src => src.Popular ?? 0)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.CyrillicName, src => src.CyrillicName)
            .Map(dest => dest.Country, src => CountryMapping.MapCountry(src.Country))
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Model, Domain.Entities.Model>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.CyrillicName, src => src.CyrillicName)
            .Map(dest => dest.Class, src => string.IsNullOrEmpty(src.Class) ? Class.None : Enum.Parse<Class>(src.Class))
            .Map(dest => dest.YearFrom, src => src.YearFrom)
            .Map(dest => dest.YearTo, src => src.YearTo)
            .Map(dest => dest.MarkId, src => src.MarkId)
            .IgnoreNonMapped(true);
        
        TypeAdapterConfig<Models.Generation, Domain.Entities.Generation>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.IsRestyle, src => src.IsRestyle)
            .Map(dest => dest.ModelId, src => src.ModelId)
            .Map(dest => dest.YearStart, src => src.YearStart)
            .Map(dest => dest.YearStop, src => src.YearStop)
            .IgnoreNonMapped(true);
        
        TypeAdapterConfig<Models.Configuration, Domain.Entities.CarConfiguration>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.GenerationId, src => src.GenerationId)
            .Map(dest => dest.BodyType, src => CountryMapping.MapBodyType(src.BodyType))
            .Map(dest => dest.ConfigurationName, src => src.ConfigurationName)
            .Map(dest => dest.DoorsCount, src => src.DoorsCount)
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Option, Comfort>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)

            .Map(dest => dest.Camera360, src => !string.IsNullOrEmpty(src._360Camera))
            .Map(dest => dest.AshtrayAndCigaretteLighter, src => !string.IsNullOrEmpty(src.AshtrayAndCigaretteLighter))
            .Map(dest => dest.AutoCruise, src => !string.IsNullOrEmpty(src.AutoCruise))
            .Map(dest => dest.AutoMirrors, src => !string.IsNullOrEmpty(src.AutoMirrors))
            .Map(dest => dest.AutoPark, src => !string.IsNullOrEmpty(src.AutoPark))
            .Map(dest => dest.CoolingBox, src => !string.IsNullOrEmpty(src.CoolingBox))
            .Map(dest => dest.CruiseControl, src => !string.IsNullOrEmpty(src.CruiseControl))
            .Map(dest => dest.EasyTrunkOpening, src => !string.IsNullOrEmpty(src.EasyTrunkOpening))
            .Map(dest => dest.ElectroMirrors, src => !string.IsNullOrEmpty(src.ElectroMirrors))
            .Map(dest => dest.ElectroTrunk, src => !string.IsNullOrEmpty(src.ElectroTrunk))
            .Map(dest => dest.ElectroWindowBack, src => !string.IsNullOrEmpty(src.ElectroWindowBack))
            .Map(dest => dest.ElectroWindowFront, src => !string.IsNullOrEmpty(src.ElectroWindowFront))
            .Map(dest => dest.FrontCamera, src => !string.IsNullOrEmpty(src.FrontCamera))
            .Map(dest => dest.RearCamera, src => !string.IsNullOrEmpty(src.RearCamera))
            .Map(dest => dest.KeylessEntry, src => !string.IsNullOrEmpty(src.KeylessEntry))
            .Map(dest => dest.Socket12V, src => !string.IsNullOrEmpty(src._12vSocket))
            .Map(dest => dest.Socket220V, src => !string.IsNullOrEmpty(src._220vSocket))
            .Map(dest => dest.AndroidAuto, src => !string.IsNullOrEmpty(src.AndroidAuto))
            .Map(dest => dest.AppleCarplay, src => !string.IsNullOrEmpty(src.AppleCarplay))
            .Map(dest => dest.AUX, src => !string.IsNullOrEmpty(src.Aux))
            .Map(dest => dest.Bluetooth, src => !string.IsNullOrEmpty(src.Bluetooth))
            .Map(dest => dest.Navigation, src => !string.IsNullOrEmpty(src.Navigation))
            .Map(dest => dest.WirelessCharger, src => !string.IsNullOrEmpty(src.WirelessCharger))
            .Map(dest => dest.YaAuto, src => !string.IsNullOrEmpty(src.YaAuto))

            .Map(dest => dest.ClimateControl, src =>
                src.ClimateControl2 != null
                    ? ClimateControl.DoubleZoned
                    : src.MultizoneClimateControl != null
                        ? ClimateControl.MultiZoned
                        : src.ClimateControl1 != null
                            ? ClimateControl.SingleZoned
                            : ClimateControl.None)
            
            .Map(dest => dest.MultiFunctionSteeringWheel, src => !string.IsNullOrEmpty(src.MultiWheel))
            .Map(dest => dest.ParkAssistFront, src => !string.IsNullOrEmpty(src.ParkAssistF))
            .Map(dest => dest.ParkAssistRear, src => !string.IsNullOrEmpty(src.ParkAssistR))
            .Map(dest => dest.PowerLatchingDoors, src => !string.IsNullOrEmpty(src.PowerLatchingDoors))
            .Map(dest => dest.ProgrammedBlockHeater, src => !string.IsNullOrEmpty(src.ProgrammedBlockHeater))
            .Map(dest => dest.ProjectionDisplay, src => !string.IsNullOrEmpty(src.ProjectionDisplay))
            .Map(dest => dest.RemoteEngineStart, src => !string.IsNullOrEmpty(src.RemoteEngineStart))
            .Map(dest => dest.StartButton, src => !string.IsNullOrEmpty(src.StartButton))
            .Map(dest => dest.StartStopFunction, src => !string.IsNullOrEmpty(src.StartStopFunction))
            .Map(dest => dest.SteeringWheelGearShiftPaddles, src => !string.IsNullOrEmpty(src.SteeringWheelGearShiftPaddles)).Map(dest => dest.AUX, src => !string.IsNullOrEmpty(src.Aux))
            .Map(dest => dest.WheelHightConfiguration, src => !string.IsNullOrEmpty(src.WheelConfiguration1))
            .Map(dest => dest.WheelDistanceConfiguration, src => !string.IsNullOrEmpty(src.WheelConfiguration2))
            .Map(dest => dest.WheelMemory, src => !string.IsNullOrEmpty(src.WheelMemory))
            .Map(dest => dest.WheelPower, src => !string.IsNullOrEmpty(src.WheelPower))
            .Map(dest => dest.AudioPreparation, src => !string.IsNullOrEmpty(src.Audiopreparation))
            .Map(dest => dest.CdAudioSystem, src => !string.IsNullOrEmpty(src.AudiosystemCd))
            .Map(dest => dest.TVAudioSystem, src => !string.IsNullOrEmpty(src.AudiosystemTv))
            .Map(dest => dest.PremiumAudio, src => !string.IsNullOrEmpty(src.MusicSuper))
            .Map(dest => dest.Multimedia, src => !string.IsNullOrEmpty(src.EntertainmentSystemForRearSeatPassengers))
            .Map(dest => dest.USB, src => !string.IsNullOrEmpty(src.Usb))
            .Map(dest => dest.VoiceRecognition, src => !string.IsNullOrEmpty(src.VoiceRecognition))
            .Map(dest => dest.SeatsHeat, src => !string.IsNullOrEmpty(src.FrontSeatsHeat))
            .Map(dest => dest.SeatSupport, src => !string.IsNullOrEmpty(src.FrontSeatSupport))
            .Map(dest => dest.DriverSeatSupport, src => !string.IsNullOrEmpty(src.DriverSeatSupport))
            .Map(dest => dest.HeatedSteeringWheel, src => !string.IsNullOrEmpty(src.WheelHeat))
            .Map(dest => dest.SeatUpDown, src => !string.IsNullOrEmpty(src.DriverSeatUpdown))
            .Map(dest => dest.ElectroRegulatingSeat, src => !string.IsNullOrEmpty(src.ElectroRearSeat))
            .Map(dest => dest.SeatsHeatVent, src => !string.IsNullOrEmpty(src.RearSeatHeatVent))
            .Map(dest => dest.MassageSeats, src => !string.IsNullOrEmpty(src.MassageSeats))
            .Map(dest => dest.Servo, src => !string.IsNullOrEmpty(src.Servo))
            .Map(dest => dest.DriveModeSystem, src => !string.IsNullOrEmpty(src.DriveModeSys))
            .Map(dest => dest.Computer, src => !string.IsNullOrEmpty(src.Computer))
            .Map(dest => dest.Condition, src => !string.IsNullOrEmpty(src.Condition))
            .Map(dest => dest.ElectronicGagePanel, src => !string.IsNullOrEmpty(src.ElectronicGagePanel))
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Specification, Interior>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.Seats, src => src.Seats)
            .Map(dest => dest.TrunksMinCapacity,
                src => !string.IsNullOrEmpty(src.TrunksMinCapacity) ? int.Parse(src.TrunksMinCapacity) : (int?)null)
            .Map(dest => dest.TrunksMaxCapacity,
                src => !string.IsNullOrEmpty(src.TrunksMaxCapacity) ? int.Parse(src.TrunksMaxCapacity) : (int?)null);
         
        TypeAdapterConfig<Models.Specification, Emissions>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.FuelEmission, src => !string.IsNullOrEmpty(src.FuelEmission) ? int.Parse(src.FuelEmission) : (float?)null)
            .Map(dest => dest.EmissionEuroClass, src => (src.EmissionEuroClass))
            .Map(dest => dest.ElectricRange, src => !string.IsNullOrEmpty(src.ElectricRange) ? int.Parse(src.ElectricRange) : (int?)null)
            .Map(dest => dest.ChargeTime, src => !string.IsNullOrEmpty(src.ChargeTime) ? float.Parse(src.ChargeTime, CultureInfo.InvariantCulture) : (float?)null)
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Option, Domain.Entities.Interior>.NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.InteriorMaterial, src =>
                src.Alcantara != null
                    ? InteriorMaterial.Alcantara
                    : src.ComboInterior != null
                        ? InteriorMaterial.ComboInterior
                        : src.FabricSeats != null
                            ? InteriorMaterial.FabricSeats
                            : src.EcoLeather != null
                                ? InteriorMaterial.EcoLeather
                                : src.Leather != null
                                    ? InteriorMaterial.Leather
                                    : InteriorMaterial.None)
            .Map(dest => dest.FrontCentreArmrest, src => string.IsNullOrEmpty(src.FrontCentreArmrest))
            .Map(dest => dest.FoldingTablesRear, src => string.IsNullOrEmpty(src.FoldingTablesRear))
            .Map(dest => dest.Hatch, src => string.IsNullOrEmpty(src.Hatch))
            .Map(dest => dest.LeatherGearStick, src => string.IsNullOrEmpty(src.LeatherGearStick))
            .Map(dest => dest.PanoramaRoof, src => string.IsNullOrEmpty(src.PanoramaRoof))
            .Map(dest => dest.RollerBlindForRearWindow, src => string.IsNullOrEmpty(src.RollerBlindForRearWindow))
            .Map(dest => dest.RollerBlindsForRearSideWindows,
                src => string.IsNullOrEmpty(src.RollerBlindsForRearSideWindows))
            .Map(dest => dest.SeatMemory, src => string.IsNullOrEmpty(src.SeatMemory))
            .Map(dest => dest.SeatTransformation, src => string.IsNullOrEmpty(src.SeatTransformation))
            .Map(dest => dest.SportPedals, src => string.IsNullOrEmpty(src.SportPedals))
            .Map(dest => dest.SportSeats, src => string.IsNullOrEmpty(src.SportSeats))
            .Map(dest => dest.ThirdRearHeadrest, src => string.IsNullOrEmpty(src.ThirdRearHeadrest))
            .Map(dest => dest.ThirdRowSeats, src => string.IsNullOrEmpty(src.ThirdRowSeats))
            .Map(dest => dest.TintedGlass, src => string.IsNullOrEmpty(src.TintedGlass))
            .Map(dest => dest.LeatherSteeringWheel, src => string.IsNullOrEmpty(src.WheelLeather))
            .Map(dest => dest.DecorativeInteriorLighting, src => string.IsNullOrEmpty(src.DecorativeInteriorLighting))
            .Map(dest => dest.BlackRoof, src => string.IsNullOrEmpty(src.BlackRoof))
            .Map(dest => dest.AdjustablePedals, src => string.IsNullOrEmpty(src.EAdjustmentWheel))
            .Map(dest => dest.FoldingFrontPassengerSeat, src => string.IsNullOrEmpty(src.FoldingFrontPassengerSeat));

        TypeAdapterConfig<Models.Option, Exterior>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)

            .Map(dest => dest.AdaptiveLight, src => !string.IsNullOrEmpty(src.AdaptiveLight))
            .Map(dest => dest.AutomaticLightingControl, src => !string.IsNullOrEmpty(src.AutomaticLightingControl))
            .Map(dest => dest.DaytimeRunningLights, src => !string.IsNullOrEmpty(src.Drl))
            .Map(dest => dest.HeatedWashSystem, src => !string.IsNullOrEmpty(src.HeatedWashSystem))
            .Map(dest => dest.HighBeamAssist, src => !string.IsNullOrEmpty(src.HighBeamAssist))
            .Map(dest => dest.LaserLights, src => !string.IsNullOrEmpty(src.LaserLights))
            .Map(dest => dest.LEDLights, src => !string.IsNullOrEmpty(src.LedLights))
            .Map(dest => dest.LightCleaner, src => !string.IsNullOrEmpty(src.LightCleaner))
            .Map(dest => dest.LightSensor, src => !string.IsNullOrEmpty(src.LightSensor))
            .Map(dest => dest.HeatedMirrors, src => !string.IsNullOrEmpty(src.MirrorsHeat))
            .Map(dest => dest.FrontFogLights, src => !string.IsNullOrEmpty(src.Ptf))
            .Map(dest => dest.RainSensor, src => !string.IsNullOrEmpty(src.RainSensor))
            .Map(dest => dest.HeatedWindshieldCleaner, src => !string.IsNullOrEmpty(src.WindcleanerHeat))
            .Map(dest => dest.HeatedWindscreen, src => !string.IsNullOrEmpty(src.WindscreenHeat))
            .Map(dest => dest.XenonLights, src => !string.IsNullOrEmpty(src.Xenon))
            .Map(dest => dest.DoubleColoredBody, src => !string.IsNullOrEmpty(src.DuoBodyColor))
            .Map(dest => dest.MetallicColored, src => !string.IsNullOrEmpty(src.PaintMetallic))
            .Map(dest => dest.RoofRails, src => !string.IsNullOrEmpty(src.RoofRails))
            .Map(dest => dest.SteelWheels, src => !string.IsNullOrEmpty(src.SteelWheels))
            .Map(dest => dest.DoorSillPanel, src => !string.IsNullOrEmpty(src.DoorSillPanel))
            .Map(dest => dest.BodyKit, src => !string.IsNullOrEmpty(src.BodyKit))
            .Map(dest => dest.Mouldings, src => !string.IsNullOrEmpty(src.BodyMouldings))
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Specification, Dimension>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.Height, src => float.Parse(src.Height, CultureInfo.InvariantCulture))
            .Map(dest => dest.Width, src => float.Parse(src.Width, CultureInfo.InvariantCulture))
            .Map(dest => dest.Length, src => float.Parse(src.Length, CultureInfo.InvariantCulture))
            .Map(dest => dest.WheelBase, src => float.Parse(src.WheelBase, CultureInfo.InvariantCulture))
            .Map(dest => dest.FrontWheelBase, src => !string.IsNullOrEmpty(src.FrontWheelBase) ? float.Parse(src.FrontWheelBase, CultureInfo.InvariantCulture) : (float?)null)
            .Map(dest => dest.BackWheelBase, src => !string.IsNullOrEmpty(src.BackWheelBase) ? float.Parse(src.BackWheelBase, CultureInfo.InvariantCulture) : (float?)null)
            .Map(dest => dest.Clearance, src => src.Clearance)
            .Map(dest => dest.WheelSize, src => src.WheelSize)
            .IgnoreNonMapped(true);
        
        TypeAdapterConfig<Models.Specification, Weight>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.BaseWeight, src => !string.IsNullOrEmpty(src.Weight) ? int.Parse(src.Weight, CultureInfo.InvariantCulture) : (int?)null)
            .Map(dest => dest.FullWeight, src => !string.IsNullOrEmpty(src.FullWeight) ? int.Parse(src.FullWeight, CultureInfo.InvariantCulture) : (int?)null)
            .Map(dest => dest.FuelTankCapacity, src =>
                string.IsNullOrEmpty(src.FuelTankCapacity) ? (int?)null : int.Parse(src.FuelTankCapacity))
            .Map(dest => dest.BatteryCapacity, src =>
                string.IsNullOrEmpty(src.BatteryCapacity) ? (float?)null : float.Parse(src.BatteryCapacity, CultureInfo.InvariantCulture))
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Specification, Safety>
            .NewConfig()
            .Map(dest => dest.SafetyRating, src => src.SafetyRating)
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.SafetyGrade,
                src => !string.IsNullOrEmpty(src.SafetyGrade) ? int.Parse(src.SafetyGrade) : (int?)null);

        TypeAdapterConfig<Models.Option, Safety>
            .NewConfig().Map(dest => dest.ModificationId, src => src.ComplectationId)

            .Map(dest => dest.ABS, src => !string.IsNullOrEmpty(src.Abs))
            .Map(dest => dest.CurtainAirbags, src => !string.IsNullOrEmpty(src.AirbagCurtain))
            .Map(dest => dest.PassengerAirbag, src => !string.IsNullOrEmpty(src.AirbagPassenger))
            .Map(dest => dest.ASR, src => !string.IsNullOrEmpty(src.Asr))
            .Map(dest => dest.BAS, src => !string.IsNullOrEmpty(src.Bas))
            .Map(dest => dest.BlindSpotMonitor, src => !string.IsNullOrEmpty(src.BlindSpot))
            .Map(dest => dest.CollisionPreventionAssist, src => !string.IsNullOrEmpty(src.CollisionPreventionAssist))
            .Map(dest => dest.DownhillAssist, src => !string.IsNullOrEmpty(src.Dha))
            .Map(dest => dest.DrowsyDriverAlertSystem, src => !string.IsNullOrEmpty(src.DrowsyDriverAlertSystem))
            .Map(dest => dest.ESP, src => !string.IsNullOrEmpty(src.Esp))
            .Map(dest => dest.FeedbackAlarm, src => !string.IsNullOrEmpty(src.FeedbackAlarm))
            .Map(dest => dest.GLONASS, src => !string.IsNullOrEmpty(src.Glonass))
            .Map(dest => dest.HillControl, src => !string.IsNullOrEmpty(src.Hcc))
            .Map(dest => dest.ISOFIX, src => !string.IsNullOrEmpty(src.Isofix))
            .Map(dest => dest.FrontISOFIX, src => !string.IsNullOrEmpty(src.IsofixFront))
            .Map(dest => dest.KneeAirbag, src => !string.IsNullOrEmpty(src.KneeAirbag))
            .Map(dest => dest.LaminatedSafetyGlass, src => !string.IsNullOrEmpty(src.LaminatedSafetyGlass))
            .Map(dest => dest.LaneKeepingAssist, src => !string.IsNullOrEmpty(src.LaneKeepingAssist))
            .Map(dest => dest.NightVision, src => !string.IsNullOrEmpty(src.NightVision))
            .Map(dest => dest.RearDoorPowerChildLocks, src => !string.IsNullOrEmpty(src.PowerChildLocksRearDoors))
            .Map(dest => dest.TrafficSignRecognition, src => !string.IsNullOrEmpty(src.TrafficSignRecognition))
            .Map(dest => dest.TyrePressureMonitoring, src => !string.IsNullOrEmpty(src.TyrePressure))
            .Map(dest => dest.VSM, src => !string.IsNullOrEmpty(src.Vsm))
            .Map(dest => dest.Alarm, src => !string.IsNullOrEmpty(src.Alarm))
            .Map(dest => dest.Immobiliser, src => !string.IsNullOrEmpty(src.Immo))
            .Map(dest => dest.Lock, src => !string.IsNullOrEmpty(src.Lock))
            .Map(dest => dest.VolumeSensor, src => !string.IsNullOrEmpty(src.VolumeSensor))
            .Map(dest => dest.DriverAirbag, src => !string.IsNullOrEmpty(src.AirbagDriver))
            .Map(dest => dest.RearSideAirbag, src => !string.IsNullOrEmpty(src.AirbagRearSide))
            .Map(dest => dest.SideAirbag, src => !string.IsNullOrEmpty(src.AirbagSide));

        TypeAdapterConfig<Models.Specification, Performance>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.TimeTo100, src => string.IsNullOrEmpty(src.TimeTo100) ? (float?)null : float.Parse(src.TimeTo100, CultureInfo.InvariantCulture))
            .Map(dest => dest.MaxSpeed, src => string.IsNullOrEmpty(src.MaxSpeed) ? (float?)null : float.Parse(src.MaxSpeed, CultureInfo.InvariantCulture))
            .Map(dest => dest.ConsumptionMixed, src =>
                string.IsNullOrEmpty(src.ConsumptionMixed) ? (float?)null : float.Parse(src.ConsumptionMixed, CultureInfo.InvariantCulture))
            .Map(dest => dest.ConsumptionCity, src =>
                string.IsNullOrEmpty(src.ConsumptionCity) ? (float?)null : float.Parse(src.ConsumptionCity, CultureInfo.InvariantCulture))
            .Map(dest => dest.RangeDistance, src =>
                string.IsNullOrEmpty(src.RangeDistance) ? (float?)null : float.Parse(src.RangeDistance, CultureInfo.InvariantCulture))
            .Map(dest => dest.ElectricRange, src =>
                string.IsNullOrEmpty(src.ElectricRange) ? (float?)null : float.Parse(src.ElectricRange, CultureInfo.InvariantCulture))
            .Map(dest => dest.ConsumptionHiway, src =>
                string.IsNullOrEmpty(src.ConsumptionHiway) ? (float?)null : float.Parse(src.ConsumptionHiway, CultureInfo.InvariantCulture))
            .IgnoreNonMapped(true);
        
        TypeAdapterConfig<Models.Modification, Domain.Entities.Modification>
            .NewConfig()
            .Map(dest => dest.Id, src => src.ComplectationId)
            .Map(dest => dest.OffersPriceTo, src => src.OffersPriceTo)
            .Map(dest => dest.GroupName, src => src.GroupName)
            .Map(dest => dest.CarConfigurationId, src => src.ConfigurationId)
            .Map(dest => dest.OffersPriceFrom, src => src.OffersPriceFrom)
            .IgnoreNonMapped(true);

        TypeAdapterConfig<Models.Specification, Mobility>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.FrontBrake, src => src.FrontBrake)
            .Map(dest => dest.BackBrake, src => src.BackBrake)
            .Map(dest => dest.FrontSuspension, src => src.FrontSuspension)
            .Map(dest => dest.BackSuspension, src => src.BackSuspension)
            .Map(dest => dest.Transmission, src => CountryMapping.MapTransmissionType(src.Transmission))
            .Map(dest => dest.Drive, src => CountryMapping.MapDriveType(src.Drive));

        TypeAdapterConfig<Models.Option, Mobility>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.ActiveSuspension, src => !string.IsNullOrEmpty(src.ActivSuspension))
            .Map(dest => dest.AirSuspension, src => !string.IsNullOrEmpty(src.AirSuspension))
            .Map(dest => dest.ReducedSpareWheel, src => !string.IsNullOrEmpty(src.ReduceSpareWheel))
            .Map(dest => dest.SpareWheel, src => !string.IsNullOrEmpty(src.SpareWheel))
            .Map(dest => dest.SportSuspension, src => !string.IsNullOrEmpty(src.SportSuspension));

        TypeAdapterConfig<Models.Specification, Engine>
            .NewConfig()
            .Map(dest => dest.ModificationId, src => src.ComplectationId)
            .Map(dest => dest.HorsePower, src => float.Parse(src.HorsePower, CultureInfo.InvariantCulture))
            .Map(dest => dest.KvtPower, src => float.Parse(src.KvtPower, CultureInfo.InvariantCulture))
            .Map(dest => dest.RpmPower, src => src.RpmPower)
            .Map(dest => dest.EngineType, src => CountryMapping.MapEngineType(src.EngineType))
            .Map(dest => dest.EngineFeeding, src => src.EngineFeeding)
            .Map(dest => dest.EngineOrder, src => src.EngineOrder)
            .Map(dest => dest.CylindersOrder, src => src.CylindersOrder)
            .Map(dest => dest.CylindersValue, src => !string.IsNullOrEmpty(src.CylindersValue) ?int.Parse(src.CylindersValue) : (int?)null)
            .Map(dest => dest.Compression, src => !string.IsNullOrEmpty(src.Compression) ? float.Parse(src.Compression, CultureInfo.InvariantCulture) : (float?)null)
            .Map(dest => dest.Volume, src => !string.IsNullOrEmpty(src.Volume) ? float.Parse(src.Volume, CultureInfo.InvariantCulture) : (float?)null)
            .Map(dest => dest.VolumeLitres, src => !string.IsNullOrEmpty(src.VolumeLitres) ? float.Parse(src.VolumeLitres, CultureInfo.InvariantCulture) : (float?)null)
            .Map(dest => dest.PetrolType, src => src.PetrolType)
            .Map(dest => dest.Valves, src => !string.IsNullOrEmpty(src.Valves) ? int.Parse(src.Valves) : (int?)null)
            .Map(dest => dest.Moment, src => !string.IsNullOrEmpty(src.Moment) ? int.Parse(src.Moment) : (int?)null)
            .Map(dest => dest.MomentRpm, src => src.MomentRpm)
            .Map(dest => dest.GearValue, src => !string.IsNullOrEmpty(src.GearValue) ? int.Parse(src.GearValue) : (int?)null)
            .Map(dest => dest.PistonStroke, src => !string.IsNullOrEmpty(src.PistonStroke) ? float.Parse(src.PistonStroke, CultureInfo.InvariantCulture) : (float?)null)
            .Map(dest => dest.Diametr, src => !string.IsNullOrEmpty(src.Diametr) ? float.Parse(src.Diametr, CultureInfo.InvariantCulture) : (float?)null)
            .IgnoreNonMapped(true);


    }
}