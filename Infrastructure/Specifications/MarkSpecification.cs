using Domain.Contracts.GetModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Enums;

namespace Infrastructure.Specifications;

/// <summary>
/// Builds query (top-down) beginning with Mark → Model → Generation and, при необходимости,
/// опускается до CarConfiguration → Modification и ниже.
/// Все include-ы и критерии добавляются динамически, исходя из содержимого GetByFilterDto.
/// Полностью заменяет прежнюю ModificationSpecification.
/// </summary>
public class MarkSpecification : Specification<Mark>
{
    public MarkSpecification(GetByFilterDto filter)
    {
        //-------------------------------------------
        // 1. Определяем, какие сущности нужны
        //-------------------------------------------
        bool needEngine      = filter.EngineType.HasValue || filter.VolumeLiters.HasValue || filter.MinHorsePower.HasValue || filter.MaxHorsePower.HasValue;
        bool needMobility    = filter.TransmissionType.HasValue || filter.DriveType.HasValue;
        bool needDimension   = filter.MinHeight.HasValue || filter.MaxHeight.HasValue || filter.MinWidth.HasValue || filter.MaxWidth.HasValue ||
                              filter.MinLength.HasValue || filter.MaxLength.HasValue || filter.FrontWheelBase.HasValue || filter.BackWheelBase.HasValue ||
                              !string.IsNullOrEmpty(filter.Clearance);
        bool needPerformance = filter.MinConsumptionMixed.HasValue || filter.MaxConsumptionMixed.HasValue;
        bool needInterior    = !string.IsNullOrEmpty(filter.Seats) || filter.TrunksMinCapacity.HasValue || filter.TrunksMaxCapacity.HasValue;
        bool needComfort = 
            filter.Camera360 != null ||
            filter.AshtrayAndCigaretteLighter != null ||
            filter.AutoCruise != null ||
            filter.AutoMirrors != null ||
            filter.AutoPark != null ||
            filter.ClimateControl != null ||
            filter.Computer != null ||
            filter.Condition != null ||
            filter.CoolingBox != null ||
            filter.CruiseControl != null ||
            filter.DriveModeSystem != null ||
            filter.EasyTrunkOpening != null ||
            filter.ElectroMirrors != null ||
            filter.ElectroTrunk != null ||
            filter.ElectroWindowBack != null ||
            filter.ElectroWindowFront != null ||
            filter.ElectronicGagePanel != null ||
            filter.FrontCamera != null ||
            filter.KeylessEntry != null ||
            filter.MultiFunctionSteeringWheel != null ||
            filter.ParkAssistFront != null ||
            filter.ParkAssistRear != null ||
            filter.PowerLatchingDoors != null ||
            filter.ProgrammedBlockHeater != null ||
            filter.ProjectionDisplay != null ||
            filter.RearCamera != null ||
            filter.RemoteEngineStart != null ||
            filter.Servo != null ||
            filter.StartButton != null ||
            filter.StartStopFunction != null ||
            filter.SteeringWheelGearShiftPaddles != null ||
            filter.WheelHightConfiguration != null ||
            filter.WheelDistanceConfiguration != null ||
            filter.WheelMemory != null ||
            filter.WheelPower != null ||
            filter.Socket12V != null ||
            filter.Socket220V != null ||
            filter.AndroidAuto != null ||
            filter.AudioPreparation != null ||
            filter.CdAudioSystem != null ||
            filter.TVAudioSystem != null ||
            filter.PremiumAudio != null ||
            filter.Multimedia != null ||
            filter.AUX != null ||
            filter.AppleCarplay != null ||
            filter.Bluetooth != null ||
            filter.Navigation != null ||
            filter.USB != null ||
            filter.VoiceRecognition != null ||
            filter.WirelessCharger != null ||
            filter.YaAuto != null ||
            filter.HeatedSteeringWheel != null ||
            filter.DriverSeatSupport != null ||
            filter.SeatUpDown != null ||
            filter.ElectroRegulatingSeat != null ||
            filter.SeatSupport != null ||
            filter.SeatsHeat != null ||
            filter.SeatsHeatVent != null ||
            filter.MassageSeats != null;

        bool needModification      = filter.MinCost.HasValue || filter.MaxCost.HasValue || needEngine || needMobility || needDimension || needPerformance || needInterior || needComfort;
        bool needCarConfiguration  = filter.BodyType.HasValue || needModification;
        //-------------------------------------------
        // 2. Добавляем include-ы
        //-------------------------------------------

        // Generation – всегда
        AddInclude(m => m.Include(mark => mark.Models)
                         .ThenInclude(model => model.Generation));

        // CarConfiguration
        AddIncludeIf(needCarConfiguration, m => m.Include(mark => mark.Models)
                                                 .ThenInclude(model => model.Generation)
                                                 .ThenInclude(gen => gen.CarConfigurations));

        // Modification
        AddIncludeIf(needModification, m => m.Include(mark => mark.Models)
                                             .ThenInclude(model => model.Generation)
                                             .ThenInclude(gen => gen.CarConfigurations)
                                             .ThenInclude(cc => cc.Modifications));

        // Навигации уровня Modification
        AddIncludeIf(needEngine,      Chain(mod => mod.Engine));
        AddIncludeIf(needMobility,    Chain(mod => mod.Mobility));
        AddIncludeIf(needDimension,   Chain(mod => mod.Dimension));
        AddIncludeIf(needPerformance, Chain(mod => mod.Performance));
        AddIncludeIf(needInterior,    Chain(mod => mod.Interior));
        AddIncludeIf(needComfort,     Chain(mod => mod.Comfort));

        //-------------------------------------------
        // 3. Критерии уровня Mark / Model / Generation / CarConfiguration
        //-------------------------------------------
        AddCriteriaIf(filter.MarkIds != null && filter.MarkIds.Any(), mark => filter.MarkIds!.Contains(mark.Id));

        AddCriteriaIf(filter.ModelIds != null && filter.ModelIds.Any(),
            mark => mark.Models.Any(model => filter.ModelIds!.Contains(model.Id)));

        AddCriteriaIf(filter.MinYear.HasValue,
            mark => mark.Models.Any(model => model.YearFrom >= filter.MinYear.Value ||
                                              model.Generation.Any(gen => gen.YearStart >= filter.MinYear.Value)));

        AddCriteriaIf(filter.MaxYear.HasValue,
            mark => mark.Models.Any(model => model.YearTo <= filter.MaxYear.Value ||
                                              model.Generation.Any(gen => gen.YearStop <= filter.MaxYear.Value)));

        AddCriteriaIf(filter.BodyType.HasValue,
            mark => mark.Models.Any(model => model.Generation.Any(gen => gen.CarConfigurations.Any(cc => cc.BodyType == filter.BodyType.Value))));

        //-------------------------------------------
        // 4. Критерии уровня Modification и ниже
        //-------------------------------------------
        // Цена
        AddCriteriaIf(filter.MinCost.HasValue || filter.MaxCost.HasValue,
            mark => mark.Models.Any(model => model.Generation.Any(gen => gen.CarConfigurations.Any(cc => cc.Modifications.Any(mod =>
                (!filter.MinCost.HasValue || mod.OffersPriceFrom >= filter.MinCost.Value) &&
                (!filter.MaxCost.HasValue || mod.OffersPriceFrom <= filter.MaxCost.Value))))));

        // Engine
        if (needEngine)
        {
            AddCriteriaIf(filter.EngineType.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Engine.EngineType == filter.EngineType.Value)))));
            AddCriteriaIf(filter.VolumeLiters.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Engine.VolumeLitres == filter.VolumeLiters.Value)))));
            AddCriteriaIf(filter.MinHorsePower.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Engine.HorsePower >= filter.MinHorsePower.Value && mod.Engine.HorsePower <= (filter.MaxHorsePower ?? mod.Engine.HorsePower))))));
        }

        // Mobility
        if (needMobility)
        {
            AddCriteriaIf(filter.TransmissionType.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Mobility.Transmission == filter.TransmissionType.Value)))));
            AddCriteriaIf(filter.DriveType.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Mobility.Drive == filter.DriveType.Value)))));
        }

        // Dimension
        if (needDimension)
        {
            AddCriteriaIf(filter.MinHeight.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Dimension.Height >= filter.MinHeight.Value && mod.Dimension.Height <= (filter.MaxHeight ?? mod.Dimension.Height))))));
            AddCriteriaIf(filter.MinWidth.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Dimension.Width >= filter.MinWidth.Value && mod.Dimension.Width <= (filter.MaxWidth ?? mod.Dimension.Width))))));
            AddCriteriaIf(filter.MinLength.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Dimension.Length >= filter.MinLength.Value && mod.Dimension.Length <= (filter.MaxLength ?? mod.Dimension.Length))))));
            AddCriteriaIf(filter.FrontWheelBase.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Dimension.FrontWheelBase == filter.FrontWheelBase)))));
            AddCriteriaIf(filter.BackWheelBase.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Dimension.BackWheelBase == filter.BackWheelBase)))));
            AddCriteriaIf(!string.IsNullOrEmpty(filter.Clearance),
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Dimension.Clearance == filter.Clearance)))));
        }

        // Performance
        if (needPerformance)
        {
            AddCriteriaIf(filter.MinConsumptionMixed.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Performance.ConsumptionMixed >= (decimal)filter.MinConsumptionMixed.Value && mod.Performance.ConsumptionMixed <= (decimal)filter.MaxConsumptionMixed.Value)))));
        }

        // Interior
        if (needInterior)
        {
            AddCriteriaIf(!string.IsNullOrEmpty(filter.Seats),
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Interior.Seats == filter.Seats)))));
            AddCriteriaIf(filter.TrunksMinCapacity.HasValue,
                mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(mod => mod.Interior.TrunksMinCapacity >= filter.TrunksMinCapacity && mod.Interior.TrunksMaxCapacity <= filter.TrunksMaxCapacity)))));
        }

        // Comfort
        if (needComfort)
        {
           

            void Comfort(bool? flag, System.Func<Domain.Entities.Modification, bool> pred)
            {
                if (flag.HasValue) AddCriteria(mark => mark.Models.Any(model =>
                    model.Generation.Any(gen =>
                        gen.CarConfigurations.Any(cc =>
                            cc.Modifications.Any(pred)))));
            }

            AddCriteriaIf(filter.Camera360.HasValue,                 mark => mark.Models.Any(model =>
                model.Generation.Any(gen =>
                    gen.CarConfigurations.Any(cc =>
                        cc.Modifications.Any(mod => mod.Comfort.Camera360 == filter.Camera360!.Value)))));
            AddCriteriaIf(filter.AshtrayAndCigaretteLighter.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AshtrayAndCigaretteLighter == filter.AshtrayAndCigaretteLighter!.Value)))));
            AddCriteriaIf(filter.AutoCruise.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AutoCruise == filter.AutoCruise!.Value)))));
            AddCriteriaIf(filter.AutoMirrors.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AutoMirrors == filter.AutoMirrors!.Value)))));
            AddCriteriaIf(filter.AutoPark.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AutoPark == filter.AutoPark!.Value)))));
            AddCriteriaIf(filter.Computer.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Computer == filter.Computer!.Value)))));
            AddCriteriaIf(filter.Condition.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Condition == filter.Condition!.Value)))));
            AddCriteriaIf(filter.CoolingBox.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.CoolingBox == filter.CoolingBox!.Value)))));
            AddCriteriaIf(filter.CruiseControl.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.CruiseControl == filter.CruiseControl!.Value)))));
            AddCriteriaIf(filter.DriveModeSystem.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.DriveModeSystem == filter.DriveModeSystem!.Value)))));
            AddCriteriaIf(filter.EasyTrunkOpening.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.EasyTrunkOpening == filter.EasyTrunkOpening!.Value)))));
            AddCriteriaIf(filter.ElectroMirrors.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ElectroMirrors == filter.ElectroMirrors!.Value)))));
            AddCriteriaIf(filter.ElectroTrunk.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ElectroTrunk == filter.ElectroTrunk!.Value)))));
            AddCriteriaIf(filter.ElectroWindowBack.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ElectroWindowBack == filter.ElectroWindowBack!.Value)))));
            AddCriteriaIf(filter.ElectroWindowFront.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ElectroWindowFront == filter.ElectroWindowFront!.Value)))));
            AddCriteriaIf(filter.ElectronicGagePanel.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ElectronicGagePanel == filter.ElectronicGagePanel!.Value)))));
            AddCriteriaIf(filter.FrontCamera.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.FrontCamera == filter.FrontCamera!.Value)))));
            AddCriteriaIf(filter.KeylessEntry.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.KeylessEntry == filter.KeylessEntry!.Value)))));
            AddCriteriaIf(filter.MultiFunctionSteeringWheel.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.MultiFunctionSteeringWheel == filter.MultiFunctionSteeringWheel!.Value)))));
            AddCriteriaIf(filter.ParkAssistFront.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ParkAssistFront == filter.ParkAssistFront!.Value)))));
            AddCriteriaIf(filter.ParkAssistRear.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ParkAssistRear == filter.ParkAssistRear!.Value)))));
            AddCriteriaIf(filter.PowerLatchingDoors.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.PowerLatchingDoors == filter.PowerLatchingDoors!.Value)))));
            AddCriteriaIf(filter.ProgrammedBlockHeater.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ProgrammedBlockHeater == filter.ProgrammedBlockHeater!.Value)))));
            AddCriteriaIf(filter.ProjectionDisplay.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ProjectionDisplay == filter.ProjectionDisplay!.Value)))));
            AddCriteriaIf(filter.RearCamera.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.RearCamera == filter.RearCamera!.Value)))));
            AddCriteriaIf(filter.RemoteEngineStart.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.RemoteEngineStart == filter.RemoteEngineStart!.Value)))));
            AddCriteriaIf(filter.Servo.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Servo == filter.Servo!.Value)))));
            AddCriteriaIf(filter.StartButton.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.StartButton == filter.StartButton!.Value)))));
            AddCriteriaIf(filter.StartStopFunction.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.StartStopFunction == filter.StartStopFunction!.Value)))));
            AddCriteriaIf(filter.SteeringWheelGearShiftPaddles.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.SteeringWheelGearShiftPaddles == filter.SteeringWheelGearShiftPaddles!.Value)))));
            AddCriteriaIf(filter.WheelMemory.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.WheelMemory == filter.WheelMemory!.Value)))));
            AddCriteriaIf(filter.WheelPower.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.WheelPower == filter.WheelPower!.Value)))));
            AddCriteriaIf(filter.Socket12V.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Socket12V == filter.Socket12V!.Value)))));
            AddCriteriaIf(filter.Socket220V.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Socket220V == filter.Socket220V!.Value)))));
            AddCriteriaIf(filter.AndroidAuto.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AndroidAuto == filter.AndroidAuto!.Value)))));
            AddCriteriaIf(filter.AudioPreparation.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AudioPreparation == filter.AudioPreparation!.Value)))));
            AddCriteriaIf(filter.CdAudioSystem.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.CdAudioSystem == filter.CdAudioSystem!.Value)))));
            AddCriteriaIf(filter.TVAudioSystem.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.TVAudioSystem == filter.TVAudioSystem!.Value)))));
            AddCriteriaIf(filter.PremiumAudio.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.PremiumAudio == filter.PremiumAudio!.Value)))));
            AddCriteriaIf(filter.Multimedia.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Multimedia == filter.Multimedia!.Value)))));
            AddCriteriaIf(filter.AUX.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AUX == filter.AUX!.Value)))));
            AddCriteriaIf(filter.AppleCarplay.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.AppleCarplay == filter.AppleCarplay!.Value)))));
            AddCriteriaIf(filter.Bluetooth.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Bluetooth == filter.Bluetooth!.Value)))));
            AddCriteriaIf(filter.Navigation.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.Navigation == filter.Navigation!.Value)))));
            AddCriteriaIf(filter.USB.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.USB == filter.USB!.Value)))));
            AddCriteriaIf(filter.VoiceRecognition.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.VoiceRecognition == filter.VoiceRecognition!.Value)))));
            AddCriteriaIf(filter.WirelessCharger.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.WirelessCharger == filter.WirelessCharger!.Value)))));
            AddCriteriaIf(filter.YaAuto.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.YaAuto == filter.YaAuto!.Value)))));
            AddCriteriaIf(filter.HeatedSteeringWheel.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.HeatedSteeringWheel == filter.HeatedSteeringWheel!.Value)))));
            AddCriteriaIf(filter.DriverSeatSupport.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.DriverSeatSupport == filter.DriverSeatSupport!.Value)))));
            AddCriteriaIf(filter.SeatUpDown.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.SeatUpDown == filter.SeatUpDown!.Value)))));
            AddCriteriaIf(filter.ElectroRegulatingSeat.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.ElectroRegulatingSeat == filter.ElectroRegulatingSeat!.Value)))));
            AddCriteriaIf(filter.SeatSupport.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.SeatSupport == filter.SeatSupport!.Value)))));
            AddCriteriaIf(filter.SeatsHeat.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.SeatsHeat == filter.SeatsHeat!.Value)))));
            AddCriteriaIf(filter.SeatsHeatVent.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.SeatsHeatVent == filter.SeatsHeatVent!.Value)))));
            AddCriteriaIf(filter.MassageSeats.HasValue, mod => mod.Models.Any(m => m.Generation.Any(g => g.CarConfigurations.Any(c => c.Modifications.Any(md => md.Comfort.MassageSeats == filter.MassageSeats!.Value)))));


            AddCriteriaIf(filter.ClimateControl != ClimateControl.None, mark => mark.Models.Any(model =>
                model.Generation.Any(gen =>
                    gen.CarConfigurations.Any(cc =>
                        cc.Modifications.Any(mod => mod.Comfort.ClimateControl == filter.ClimateControl)))));
            AddCriteriaIf(!string.IsNullOrEmpty(filter.WheelHightConfiguration),  mark => mark.Models.Any(model =>
                model.Generation.Any(gen =>
                    gen.CarConfigurations.Any(cc =>
                        cc.Modifications.Any(mod => mod.Comfort.WheelHightConfiguration == filter.WheelHightConfiguration)))));
            AddCriteriaIf(!string.IsNullOrEmpty(filter.WheelDistanceConfiguration),mark => mark.Models.Any(model =>
                model.Generation.Any(gen =>
                    gen.CarConfigurations.Any(cc =>
                        cc.Modifications.Any(mod => mod.Comfort.WheelDistanceConfiguration == filter.WheelDistanceConfiguration)))));
        }
    }

    // Helper: builds include chain to specific nav property
    private static Func<IQueryable<Mark>, IQueryable<Mark>> Chain(System.Linq.Expressions.Expression<System.Func<Domain.Entities.Modification, object>> nav)
    {
        return m => m.Include(mark => mark.Models)
                      .ThenInclude(model => model.Generation)
                      .ThenInclude(gen => gen.CarConfigurations)
                      .ThenInclude(cc => cc.Modifications)
                      .ThenInclude(nav);
    }

    // Helper: builds Any() predicate to Modification level
} 