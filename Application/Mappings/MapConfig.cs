using Application.Dto_s.Responses;
using Application.UseCases.Queries;
using Domain.Contracts.CleanModels;
using Domain.Contracts.GetModels;
using Domain.Entities.CustomEntities;
using Mapster;
using System.Collections.Generic;
using Application.Dto_s.Models;
using Application.Dto_s.Models.HelpModels;
using Generation = Domain.Entities.Generation;
using Mark = Domain.Entities.Mark;
using Model = Domain.Entities.Model;
using Modification = Domain.Entities.Modification;

namespace Application.Mappings;

public static class MapConfig
{
    public static void Register()
    {
        TypeAdapterConfig<Mark, MarkClean>.NewConfig()
            .Map(destination => destination.CyrillicName, source => source.CyrillicName)
            .Map(destination => destination.Country, source => source.Country)
            .Map(destination => destination.Popular, source => source.Popular)
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.Id, source => source.Id);

        TypeAdapterConfig<Domain.Entities.Comfort, Comfort>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Dimension, Dimension>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Emissions, Emissions>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Exterior, Exterior>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Mobility, Mobility>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Engine, Engine>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Interior, Interior>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Performance, Performance>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Safety, Safety>.NewConfig();
        TypeAdapterConfig<Domain.Entities.Weight, Weight>.NewConfig();
        
        TypeAdapterConfig<Model, ModelClean>.NewConfig()
            .Map(destination => destination.CyrillicName, source => source.CyrillicName)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.MarkId, source => source.MarkId)
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.YearFrom, source => source.YearFrom)
            .Map(destination => destination.YearTo, source => source.YearTo)
            .Map(destination => destination.Class, source => source.Class);
        
        TypeAdapterConfig<Generation, GenerationClean>.NewConfig()
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.IsRestyle, source => source.IsRestyle)
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.YearStart, source => source.YearStart)
            .Map(destination => destination.YearStop, source => source.YearStop)
            .Map(destination => destination.ModelId, source => source.ModelId);
        
        TypeAdapterConfig<Domain.Entities.CarConfiguration, CarConfigurationClean>.NewConfig()
            .Map(destination => destination.GenerationId, source => source.GenerationId)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.BodyType, source => source.BodyType)
            .Map(destination => destination.ConfigurationName, source => source.ConfigurationName)
            .Map(destination => destination.DoorsCount, source => source.DoorsCount);

        
        //dto's
        TypeAdapterConfig<Mark, Dto_s.Models.Mark>.NewConfig()
            .Map(destination => destination.CyrillicName, source => source.CyrillicName)
            .Map(destination => destination.Country, source => source.Country)
            .Map(destination => destination.Models, source => source.Models.Adapt<IEnumerable<Dto_s.Models.Model>>())
            .Map(destination => destination.Popular, source => source.Popular)
            .Map(destination => destination.Id, source => source.Id);
        
        TypeAdapterConfig<Model, Dto_s.Models.Model>.NewConfig()
            .Map(destination => destination.CyrillicName, source => source.CyrillicName)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.MarkId, source => source.MarkId)
            .Map(destination => destination.Generation, source => source.Generation.Adapt<IEnumerable<Dto_s.Models.Generation>>())
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.YearFrom, source => source.YearFrom)
            .Map(destination => destination.YearTo, source => source.YearTo)
            .Map(destination => destination.Class, source => source.Class);
        
        TypeAdapterConfig<Generation, Dto_s.Models.Generation>.NewConfig()
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.IsRestyle, source => source.IsRestyle)
            .Map(destination => destination.CarConfigurations, source => source.CarConfigurations.Adapt<IEnumerable<Dto_s.Models.CarConfiguration>>())
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.YearStart, source => source.YearStart)
            .Map(destination => destination.YearStop, source => source.YearStop)
            .Map(destination => destination.ModelId, source => source.ModelId);
        
        TypeAdapterConfig<Domain.Entities.CarConfiguration, Dto_s.Models.CarConfiguration>.NewConfig()
            .Map(destination => destination.GenerationId, source => source.GenerationId)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.BodyType, source => source.BodyType)
            .Map(destination => destination.ConfigurationName, source => source.ConfigurationName)
            .Map(destination => destination.DoorsCount, source => source.DoorsCount)
            .Map(destination => destination.Modifications, source => source.Modifications.Adapt<IEnumerable<Dto_s.Models.Modification>>());
        
        /*TypeAdapterConfig<Domain.Entities.CarConfiguration, Dto_s.Models.CarConfiguration>.NewConfig()
            .Map(destination => destination.GenerationId, source => source.GenerationId)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.BodyType, source => source.BodyType)
            .Map(destination => destination.ConfigurationName, source => source.ConfigurationName)
            .Map(destination => destination.DoorsCount, source => source.DoorsCount)
            .Map(destination => destination.Modifications, source => source.Modifications.Adapt<IEnumerable<Dto_s.Models.ModificationSimple>>());*/

        TypeAdapterConfig<Modification, Dto_s.Models.Modification>.NewConfig()
            .Map(destination => destination.OffersPriceFrom, source => source.OffersPriceFrom)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.OffersPriceTo, source => source.OffersPriceTo)
            .Map(destination => destination.Comfort, source => source.Comfort.Adapt<Dto_s.Models.Comfort>())
            .Map(destination => destination.Dimension, source => source.Dimension.Adapt<Dto_s.Models.Dimension>())
            .Map(destination => destination.Emissions, source => source.Emissions.Adapt<Dto_s.Models.Emissions>())
            .Map(destination => destination.Mobility, source => source.Mobility.Adapt<Dto_s.Models.Mobility>())
            .Map(destination => destination.Engine, source => source.Engine.Adapt<Dto_s.Models.Engine>())
            .Map(destination => destination.Exterior, source => source.Exterior.Adapt<Dto_s.Models.Exterior>())
            .Map(destination => destination.Interior, source => source.Interior.Adapt<Dto_s.Models.Interior>())
            .Map(destination => destination.Performance, source => source.Performance.Adapt<Dto_s.Models.Performance>())
            .Map(destination => destination.Safety, source => source.Safety.Adapt<Dto_s.Models.Safety>())
            .Map(destination => destination.Weight, source => source.Weight.Adapt<Dto_s.Models.Weight>())
            .Map(destination => destination.GroupName, source => source.GroupName)
            .Map(destination => destination.CarConfigurationId, source => source.CarConfigurationId);
        
        
        //Responses
        TypeAdapterConfig<Modification, ModificationSimple>.NewConfig()
            .Map(destination => destination.OffersPriceFrom, source => source.OffersPriceFrom)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.OffersPriceTo, source => source.OffersPriceTo)
            .Map(destination => destination.Mobility, source => source.Mobility.Adapt<Dto_s.Models.Mobility>())
            .Map(destination => destination.Engine, source => source.Engine.Adapt<Dto_s.Models.Engine>())
            .Map(destination => destination.GroupName, source => source.GroupName)
            .Map(destination => destination.CarConfigurationId, source => source.CarConfigurationId);
        
        TypeAdapterConfig<Domain.Entities.CarConfiguration, GetConfigurationFullInfoResponse>.NewConfig()
            .Map(destination => destination.Mark, source => source.Generation.Model.Mark.Adapt<Dto_s.Models.Mark>());

        TypeAdapterConfig<Domain.Entities.Mark, GetByFilterResponse>.NewConfig()
            .Map(dest => dest.Marks, src => src.Adapt<Application.Dto_s.Models.Mark>());
        
        TypeAdapterConfig<Mark, GetMarksResponse>.NewConfig()
            .Map(destination => destination.Country, source => source.Country)
            .Map(destination => destination.CyrillicName, source => source.CyrillicName)
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.Popular, source => source.Popular);

        TypeAdapterConfig<Mark, GetMarkConfigurationsResponse>.NewConfig()
            .Map(destination => destination.Id, source => source.Id)
            .Map(destination => destination.CyrillicName, source => source.CyrillicName)
            .Map(destination => destination.Name, source => source.Name)
            .Map(destination => destination.Models, source => source.Models);
        
        TypeAdapterConfig<Modification, GetModificationInfoResponse>.NewConfig()
            .Map(destination => destination.Comfort, source => source.Comfort)
            .Map(destination => destination.Dimension, source => source.Dimension)
            .Map(destination => destination.Emissions, source => source.Emissions)
            .Map(destination => destination.Engine, source => source.Engine)
            .Map(destination => destination.Exterior, source => source.Exterior)
            .Map(destination => destination.Interior, source => source.Interior)
            .Map(destination => destination.Mobility, source => source.Mobility)
            .Map(destination => destination.Performance, source => source.Performance)
            .Map(destination => destination.Mobility, source => source.Mobility)
            .Map(destination => destination.Safety, source => source.Safety)
            .Map(destination => destination.Weight, source => source.Weight)
            .Map(destination => destination.GroupName, source => source.GroupName)
            .Map(destination => destination.OffersPriceFrom, source => source.OffersPriceFrom)
            .Map(destination => destination.OffersPriceTo, source => source.OffersPriceTo)
            .Map(destination => destination.Id, source => source.Id);

        TypeAdapterConfig<Generation, GetRandomCarsResponse>.NewConfig()
            .Map(destination => destination.MarkId, source => source.Model.Mark.Id)
            .Map(destination => destination.MarkName, source => source.Model.Mark.Name)
            .Map(destination => destination.MarkCyrillicName, source => source.Model.Mark.CyrillicName)
            .Map(destination => destination.Country, source => source.Model.Mark.Country)
            .Map(destination => destination.Popular, source => source.Model.Mark.Popular)
            .Map(destination => destination.ModelId, source => source.Model.Id)
            .Map(destination => destination.ModelName, source => source.Model.Name)
            .Map(destination => destination.ModelCyrillicName, source => source.Model.CyrillicName)
            .Map(destination => destination.Class, source => source.Model.Class)
            .Map(destination => destination.YearFrom, source => source.Model.YearFrom)
            .Map(destination => destination.YearTo, source => source.Model.YearTo)
            .Map(destination => destination.GenerationId, source => source.Id)
            .Map(destination => destination.GenerationName, source => source.Name)
            .Map(destination => destination.YearStart, source => source.YearStart)
            .Map(destination => destination.YearStop, source => source.YearStop)
            .Map(destination => destination.IsRestyle, source => source.IsRestyle)
            .Map(destination => destination.CarConfigurationId, source => source.CarConfigurations.First().GenerationId)
            .Map(destination => destination.DoorsCount, source => source.CarConfigurations.First().DoorsCount)
            .Map(destination => destination.BodyType, source => source.CarConfigurations.First().BodyType)
            .Map(destination => destination.ConfigurationName, source => source.CarConfigurations.First().ConfigurationName)
            .Map(destination => destination.ModificationId, source => source.CarConfigurations.First().Modifications.First().Id)
            .Map(destination => destination.OffersPriceFrom, source => source.CarConfigurations.First().Modifications.First().OffersPriceFrom)
            .Map(destination => destination.OffersPriceTo, source => source.CarConfigurations.First().Modifications.First().OffersPriceTo)
            .Map(destination => destination.GroupName, source => source.CarConfigurations.First().Modifications.First().GroupName)
            .Map(destination => destination.Engine, source => source.CarConfigurations.First().Modifications.First().Engine)
            .Map(destination => destination.Mobility, source => source.CarConfigurations.First().Modifications.First().Mobility);

        
        TypeAdapterConfig<WeeklyCar, GetWeeklyCarsResponse>.NewConfig()
            .Map(destination => destination.GenerationId, source => source.GenerationId)
            .Map(destination => destination.MarkId, source => source.MarkId)
            .Map(destination => destination.MarkName, source => source.MarkName)
            .Map(destination => destination.ModelName, source => source.ModelName)
            .Map(destination => destination.ModelId, source => source.ModelId);
        
        TypeAdapterConfig<WeeklyCar, UpdateWeeklyCarsResponse>.NewConfig()
            .Map(destination => destination.GenerationId, source => source.GenerationId)
            .Map(destination => destination.MarkId, source => source.MarkId)
            .Map(destination => destination.MarkName, source => source.MarkName)
            .Map(destination => destination.ModelName, source => source.ModelName)
            .Map(destination => destination.ModelId, source => source.ModelId);
        
        TypeAdapterConfig<GetByFilterQuery, GetByFilterDto>.NewConfig()
            .Map(destination => destination.MarkIds, source => source.MarkIds)
            .Map(destination => destination.ModelIds, source => source.ModelIds)
            .Map(destination => destination.MaxWidth, source => source.MaxWidth)
            .Map(destination => destination.MinWidth, source => source.MinWidth)
            .Map(destination => destination.MinCost, source => source.MinCost)
            .Map(destination => destination.MaxCost, source => source.MaxCost)
            .Map(destination => destination.MinHeight, source => source.MinHeight)
            .Map(destination => destination.MaxHeight, source => source.MaxHeight)
            .Map(destination => destination.MinLength, source => source.MinLength)
            .Map(destination => destination.MaxLength, source => source.MaxLength)
            .Map(destination => destination.MinYear, source => source.MinYear)
            .Map(destination => destination.MaxYear, source => source.MaxYear)
            .Map(destination => destination.MinConsumptionMixed, source => source.MinConsumptionMixed)
            .Map(destination => destination.MaxConsumptionMixed, source => source.MaxConsumptionMixed)
            .Map(destination => destination.MinHorsePower, source => source.MaxHorsePower)
            .Map(destination => destination.TrunksMinCapacity, source => source.TrunksMinCapacity)
            .Map(destination => destination.TrunksMaxCapacity, source => source.TrunksMaxCapacity)
            .Map(destination => destination.PageNumber, source => source.PageNumber)
            .Map(destination => destination.PageSize, source => source.PageSize)
            .Map(destination => destination.Camera360, source => source.Camera360)
            .Map(destination => destination.AshtrayAndCigaretteLighter, source => source.AshtrayAndCigaretteLighter)
            .Map(destination => destination.AutoCruise, source => source.AutoCruise)
            .Map(destination => destination.AutoMirrors, source => source.AutoMirrors)
            .Map(destination => destination.AutoPark, source => source.AutoPark)
            .Map(destination => destination.ClimateControl, source => source.ClimateControl)
            .Map(destination => destination.Computer, source => source.Computer)
            .Map(destination => destination.Condition, source => source.Condition)
            .Map(destination => destination.CoolingBox, source => source.CoolingBox)
            .Map(destination => destination.CruiseControl, source => source.CruiseControl)
            .Map(destination => destination.DriveModeSystem, source => source.DriveModeSystem)
            .Map(destination => destination.EasyTrunkOpening, source => source.EasyTrunkOpening)
            .Map(destination => destination.ElectroMirrors, source => source.ElectroMirrors)
            .Map(destination => destination.ElectroTrunk, source => source.ElectroTrunk)
            .Map(destination => destination.ElectroWindowBack, source => source.ElectroWindowBack)
            .Map(destination => destination.ElectroWindowFront, source => source.ElectroWindowFront)
            .Map(destination => destination.ElectronicGagePanel, source => source.ElectronicGagePanel)
            .Map(destination => destination.FrontCamera, source => source.FrontCamera)
            .Map(destination => destination.KeylessEntry, source => source.KeylessEntry)
            .Map(destination => destination.MultiFunctionSteeringWheel, source => source.MultiFunctionSteeringWheel)
            .Map(destination => destination.ParkAssistFront, source => source.ParkAssistFront)
            .Map(destination => destination.ParkAssistRear, source => source.ParkAssistRear)
            .Map(destination => destination.PowerLatchingDoors, source => source.PowerLatchingDoors)
            .Map(destination => destination.ProgrammedBlockHeater, source => source.ProgrammedBlockHeater)
            .Map(destination => destination.ProjectionDisplay, source => source.ProjectionDisplay)
            .Map(destination => destination.RearCamera, source => source.RearCamera)
            .Map(destination => destination.RemoteEngineStart, source => source.RemoteEngineStart)
            .Map(destination => destination.Servo, source => source.Servo)
            .Map(destination => destination.StartButton, source => source.StartButton)
            .Map(destination => destination.StartStopFunction, source => source.StartStopFunction)
            .Map(destination => destination.SteeringWheelGearShiftPaddles, source => source.SteeringWheelGearShiftPaddles)
            .Map(destination => destination.WheelHightConfiguration, source => source.WheelHightConfiguration)
            .Map(destination => destination.WheelDistanceConfiguration, source => source.WheelDistanceConfiguration)
            .Map(destination => destination.WheelMemory, source => source.WheelMemory)
            .Map(destination => destination.WheelPower, source => source.WheelPower)
            .Map(destination => destination.Socket12V, source => source.Socket12V)
            .Map(destination => destination.Socket220V, source => source.Socket220V)
            .Map(destination => destination.AndroidAuto, source => source.AndroidAuto)
            .Map(destination => destination.AudioPreparation, source => source.AudioPreparation)
            .Map(destination => destination.CdAudioSystem, source => source.CdAudioSystem)
            .Map(destination => destination.TVAudioSystem, source => source.TVAudioSystem)
            .Map(destination => destination.PremiumAudio, source => source.PremiumAudio)
            .Map(destination => destination.Multimedia, source => source.Multimedia)
            .Map(destination => destination.AUX, source => source.AUX)
            .Map(destination => destination.AppleCarplay, source => source.AppleCarplay)
            .Map(destination => destination.Bluetooth, source => source.Bluetooth)
            .Map(destination => destination.Navigation, source => source.Navigation)
            .Map(destination => destination.USB, source => source.USB)
            .Map(destination => destination.VoiceRecognition, source => source.VoiceRecognition)
            .Map(destination => destination.WirelessCharger, source => source.WirelessCharger)
            .Map(destination => destination.YaAuto, source => source.YaAuto)
            .Map(destination => destination.HeatedSteeringWheel, source => source.HeatedSteeringWheel)
            .Map(destination => destination.DriverSeatSupport, source => source.DriverSeatSupport)
            .Map(destination => destination.SeatUpDown, source => source.SeatUpDown)
            .Map(destination => destination.ElectroRegulatingSeat, source => source.ElectroRegulatingSeat)
            .Map(destination => destination.SeatSupport, source => source.SeatSupport)
            .Map(destination => destination.SeatsHeat, source => source.SeatsHeat)
            .Map(destination => destination.SeatsHeatVent, source => source.SeatsHeatVent)
            .Map(destination => destination.MassageSeats, source => source.MassageSeats);
    }
}