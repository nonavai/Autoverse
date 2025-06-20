/*using Domain.Contracts.GetModels;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using DriveType = Domain.Enums.DriveType;

namespace Infrastructure.Specifications;

public class ModificationSpecification : Specification<Modification>
{
    public ModificationSpecification(GetByFilterDto filter)
    {
        AddInclude(m =>
            m.Include(modification => modification.CarConfiguration)
                .ThenInclude(m => m.Generation)
                .ThenInclude(m => m.Model)
                .ThenInclude(m => m.Mark));
        
        AddCriteriaIf(filter.MarkIds != null && filter.MarkIds.Any(),m => filter.MarkIds.Contains(m.CarConfiguration.Generation.Model.Mark.Id));
        AddCriteriaIf(filter.ModelIds != null && filter.ModelIds.Any(),m => filter.ModelIds.Contains(m.CarConfiguration.Generation.Model.Id));
        AddCriteriaIf(filter.BodyType.HasValue,m => m.CarConfiguration.BodyType == filter.BodyType.Value);
        AddCriteriaIf(filter.MinCost.HasValue || filter.MaxCost.HasValue,m => (!filter.MinCost.HasValue || m.OffersPriceFrom >= filter.MinCost.Value) && (!filter.MaxCost.HasValue || m.OffersPriceFrom >= filter.MaxCost.Value));

        if (filter.EngineType.HasValue || filter.VolumeLiters.HasValue || filter.MinHorsePower.HasValue)
        {
            AddInclude(m => m.Include(m => m.Engine));
            AddCriteriaIf(filter.EngineType.HasValue,m => m.Engine.EngineType == filter.EngineType.Value);
            AddCriteriaIf(filter.VolumeLiters.HasValue,m => m.Engine.VolumeLitres == filter.VolumeLiters.Value);
            AddCriteriaIf(filter.MinHorsePower.HasValue,m => m.Engine.HorsePower >= filter.MinHorsePower.Value && m.Engine.HorsePower <= filter.MaxHorsePower.Value);
        }
        if (filter.TransmissionType.HasValue || filter.DriveType.HasValue)
        {
            AddInclude(m => m.Include(m => m.Mobility));
            AddCriteriaIf(filter.TransmissionType.HasValue,m => m.Mobility.Transmission == filter.TransmissionType.Value);
            AddCriteriaIf(filter.DriveType.HasValue,m => m.Mobility.Drive == filter.DriveType.Value);
        }
        if (filter.MinHeight.HasValue || filter.MinWidth.HasValue || filter.MinLength.HasValue || filter.FrontWheelBase.HasValue || filter.BackWheelBase.HasValue || !string.IsNullOrEmpty(filter.Clearance))
        {
            AddInclude(m => m.Include(m => m.Dimension));
            AddCriteriaIf(filter.MinHeight.HasValue, m => m.Dimension.Height >= filter.MinHeight.Value && m.Dimension.Height <= filter.MaxHeight.Value);
            AddCriteriaIf(filter.MinWidth.HasValue, m => m.Dimension.Width >= filter.MinWidth.Value && m.Dimension.Width <= filter.MaxWidth.Value);
            AddCriteriaIf(filter.MinLength.HasValue, m => m.Dimension.Length >= filter.MinLength.Value && m.Dimension.Length <= filter.MaxLength.Value);
            AddCriteriaIf(filter.FrontWheelBase.HasValue, m => Equals(m.Dimension.FrontWheelBase, filter.FrontWheelBase.Value));
            AddCriteriaIf(filter.BackWheelBase.HasValue, m => Equals(m.Dimension.BackWheelBase, filter.BackWheelBase.Value));
            AddCriteriaIf(!string.IsNullOrEmpty(filter.Clearance), m => Equals(m.Dimension.Clearance, filter.Clearance));
        }
        if (filter.MinConsumptionMixed.HasValue)
        {
            AddInclude(m => m.Include(m => m.Performance));
            AddCriteriaIf(filter.MinConsumptionMixed.HasValue,m => m.Performance.ConsumptionMixed >= (decimal)filter.MinConsumptionMixed.Value && m.Performance.ConsumptionMixed <= (decimal)filter.MaxConsumptionMixed.Value);
        }
        if (filter.MinConsumptionMixed.HasValue)
        {
            AddInclude(m => m.Include(m => m.Performance));
            AddCriteriaIf(filter.MinConsumptionMixed.HasValue,m => m.Performance.ConsumptionMixed >= (decimal)filter.MinConsumptionMixed.Value && m.Performance.ConsumptionMixed <= (decimal)filter.MaxConsumptionMixed.Value);
        }
        if (!string.IsNullOrEmpty(filter.Seats) || filter.TrunksMinCapacity.HasValue)
        {
            AddInclude(m => m.Include(m => m.Interior));
            AddCriteriaIf(filter.Seats != null,m => m.Interior.Seats == filter.Seats);
            AddCriteriaIf(filter.TrunksMinCapacity.HasValue,m => m.Interior.TrunksMinCapacity >= filter.TrunksMinCapacity && m.Interior.TrunksMaxCapacity <= filter.TrunksMaxCapacity);
        }

        if (filter.Comfort != null)
        {
            AddCriteriaIf(filter.Comfort.Camera360, m => m.Comfort.Camera360);
            AddCriteriaIf(filter.Comfort.AshtrayAndCigaretteLighter, m => m.Comfort.AshtrayAndCigaretteLighter);
            AddCriteriaIf(filter.Comfort.AutoCruise, m => m.Comfort.AutoCruise);
            AddCriteriaIf(filter.Comfort.AutoMirrors, m => m.Comfort.AutoMirrors);
            AddCriteriaIf(filter.Comfort.AutoPark, m => m.Comfort.AutoPark);
            AddCriteriaIf(filter.Comfort.Computer, m => m.Comfort.Computer);
            AddCriteriaIf(filter.Comfort.Condition, m => m.Comfort.Condition);
            AddCriteriaIf(filter.Comfort.CoolingBox, m => m.Comfort.CoolingBox);
            AddCriteriaIf(filter.Comfort.CruiseControl, m => m.Comfort.CruiseControl);
            AddCriteriaIf(filter.Comfort.DriveModeSystem, m => m.Comfort.DriveModeSystem);
            AddCriteriaIf(filter.Comfort.EasyTrunkOpening, m => m.Comfort.EasyTrunkOpening);
            AddCriteriaIf(filter.Comfort.ElectroMirrors, m => m.Comfort.ElectroMirrors);
            AddCriteriaIf(filter.Comfort.ElectroTrunk, m => m.Comfort.ElectroTrunk);
            AddCriteriaIf(filter.Comfort.ElectroWindowBack, m => m.Comfort.ElectroWindowBack);
            AddCriteriaIf(filter.Comfort.ElectroWindowFront, m => m.Comfort.ElectroWindowFront);
            AddCriteriaIf(filter.Comfort.ElectronicGagePanel, m => m.Comfort.ElectronicGagePanel);
            AddCriteriaIf(filter.Comfort.FrontCamera, m => m.Comfort.FrontCamera);
            AddCriteriaIf(filter.Comfort.KeylessEntry, m => m.Comfort.KeylessEntry);
            AddCriteriaIf(filter.Comfort.MultiFunctionSteeringWheel, m => m.Comfort.MultiFunctionSteeringWheel);
            AddCriteriaIf(filter.Comfort.ParkAssistFront, m => m.Comfort.ParkAssistFront);
            AddCriteriaIf(filter.Comfort.ParkAssistRear, m => m.Comfort.ParkAssistRear);
            AddCriteriaIf(filter.Comfort.PowerLatchingDoors, m => m.Comfort.PowerLatchingDoors);
            AddCriteriaIf(filter.Comfort.ProgrammedBlockHeater, m => m.Comfort.ProgrammedBlockHeater);
            AddCriteriaIf(filter.Comfort.ProjectionDisplay, m => m.Comfort.ProjectionDisplay);
            AddCriteriaIf(filter.Comfort.RearCamera, m => m.Comfort.RearCamera);
            AddCriteriaIf(filter.Comfort.RemoteEngineStart, m => m.Comfort.RemoteEngineStart);
            AddCriteriaIf(filter.Comfort.Servo, m => m.Comfort.Servo);
            AddCriteriaIf(filter.Comfort.StartButton, m => m.Comfort.StartButton);
            AddCriteriaIf(filter.Comfort.StartStopFunction, m => m.Comfort.StartStopFunction);
            AddCriteriaIf(filter.Comfort.SteeringWheelGearShiftPaddles, m => m.Comfort.SteeringWheelGearShiftPaddles);
            AddCriteriaIf(filter.Comfort.WheelMemory, m => m.Comfort.WheelMemory);
            AddCriteriaIf(filter.Comfort.WheelPower, m => m.Comfort.WheelPower);
            AddCriteriaIf(filter.Comfort.Socket12V, m => m.Comfort.Socket12V);
            AddCriteriaIf(filter.Comfort.Socket220V, m => m.Comfort.Socket220V);
            AddCriteriaIf(filter.Comfort.AndroidAuto, m => m.Comfort.AndroidAuto);
            AddCriteriaIf(filter.Comfort.AudioPreparation, m => m.Comfort.AudioPreparation);
            AddCriteriaIf(filter.Comfort.CdAudioSystem, m => m.Comfort.CdAudioSystem);
            AddCriteriaIf(filter.Comfort.TVAudioSystem, m => m.Comfort.TVAudioSystem);
            AddCriteriaIf(filter.Comfort.PremiumAudio, m => m.Comfort.PremiumAudio);
            AddCriteriaIf(filter.Comfort.Multimedia, m => m.Comfort.Multimedia);
            AddCriteriaIf(filter.Comfort.AUX, m => m.Comfort.AUX);
            AddCriteriaIf(filter.Comfort.AppleCarplay, m => m.Comfort.AppleCarplay);
            AddCriteriaIf(filter.Comfort.Bluetooth, m => m.Comfort.Bluetooth);
            AddCriteriaIf(filter.Comfort.Navigation, m => m.Comfort.Navigation);
            AddCriteriaIf(filter.Comfort.USB, m => m.Comfort.USB);
            AddCriteriaIf(filter.Comfort.VoiceRecognition, m => m.Comfort.VoiceRecognition);
            AddCriteriaIf(filter.Comfort.WirelessCharger, m => m.Comfort.WirelessCharger);
            AddCriteriaIf(filter.Comfort.YaAuto, m => m.Comfort.YaAuto);
            AddCriteriaIf(filter.Comfort.HeatedSteeringWheel, m => m.Comfort.HeatedSteeringWheel);
            AddCriteriaIf(filter.Comfort.DriverSeatSupport, m => m.Comfort.DriverSeatSupport);
            AddCriteriaIf(filter.Comfort.SeatUpDown, m => m.Comfort.SeatUpDown);
            AddCriteriaIf(filter.Comfort.ElectroRegulatingSeat, m => m.Comfort.ElectroRegulatingSeat);
            AddCriteriaIf(filter.Comfort.SeatSupport, m => m.Comfort.SeatSupport);
            AddCriteriaIf(filter.Comfort.SeatsHeat, m => m.Comfort.SeatsHeat);
            AddCriteriaIf(filter.Comfort.SeatsHeatVent, m => m.Comfort.SeatsHeatVent);
            AddCriteriaIf(filter.Comfort.MassageSeats, m => m.Comfort.MassageSeats);
            
            AddCriteriaIf(filter.Comfort.ClimateControl != ClimateControl.None, 
                m => m.Comfort.ClimateControl == filter.Comfort.ClimateControl);
            
            AddCriteriaIf(!string.IsNullOrEmpty(filter.Comfort.WheelHightConfiguration), 
                m => m.Comfort.WheelHightConfiguration == filter.Comfort.WheelHightConfiguration);
                
            AddCriteriaIf(!string.IsNullOrEmpty(filter.Comfort.WheelDistanceConfiguration), 
                m => m.Comfort.WheelDistanceConfiguration == filter.Comfort.WheelDistanceConfiguration);
        }
    }
}*/