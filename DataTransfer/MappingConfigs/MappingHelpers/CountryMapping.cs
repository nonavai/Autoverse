using Domain.Enums;
using DriveType = Domain.Enums.DriveType;

namespace DataTransfer.MappingConfigs.MappingHelpers;

public static class CountryMapping
{
    public static Country MapCountry(string countryName)
    {
        var countryMapping = new Dictionary<string, Country>(StringComparer.OrdinalIgnoreCase)
        {
            {"Австралия", Country.Australia},
            {"Австрия", Country.Austria},
            {"Беларусь", Country.Belarus},
            {"Бразилия", Country.Brazil},
            {"Великобритания", Country.UnitedKingdom},
            {"Вьетнам", Country.Vietnam},
            {"Германия", Country.Germany},
            {"Дания", Country.Denmark},
            {"Израиль", Country.Israel},
            {"Индия", Country.India},
            {"Иран", Country.Iran},
            {"Испания", Country.Spain},
            {"Италия", Country.Italy},
            {"Китай", Country.China},
            {"Латвия", Country.Latvia},
            {"Малайзия", Country.Malaysia},
            {"Мексика", Country.Mexico},
            {"Нидерланды", Country.Netherlands},
            {"Норвегия", Country.Norway},
            {"Объединённые Арабские Эмираты", Country.UnitedArabEmirates},
            {"Польша", Country.Poland},
            {"Россия", Country.Russia},
            {"Румыния", Country.Romania},
            {"США", Country.UnitedStates},
            {"Северная Америка", Country.NorthAmerica},
            {"Сербия", Country.Serbia},
            {"Таиланд", Country.Thailand},
            {"Тайвань", Country.Taiwan},
            {"Турция", Country.Turkey},
            {"Узбекистан", Country.Uzbekistan},
            {"Украина", Country.Ukraine},
            {"Франция", Country.France},
            {"Хорватия", Country.Croatia},
            {"Чехия", Country.CzechRepublic},
            {"Швейцария", Country.Switzerland},
            {"Швеция", Country.Sweden},
            {"Южная Корея", Country.SouthKorea},
            {"Япония", Country.Japan}
        };

        if (countryMapping.TryGetValue(countryName, out var country))
        {
            return country;
        }

        throw new ArgumentException($"Unknown country: {countryName}");
    }

    public static BodyType MapBodyType(string? bodyType)
    {
        if (string.IsNullOrEmpty(bodyType))
            return BodyType.None;

        var bodyTypeMapping = new Dictionary<string, BodyType>(StringComparer.OrdinalIgnoreCase)
        {
            {"хэтчбек 5 дв.", BodyType.Hatchback5Door},
            {"компактвэн", BodyType.CompactVan},
            {"лифтбек", BodyType.Liftback},
            {"седан", BodyType.Sedan},
            {"купе", BodyType.Coupe},
            {"внедорожник 5 дв.", BodyType.SUV5Door},
            {"родстер", BodyType.Roadster},
            {"кабриолет", BodyType.Convertible},
            {"пикап двойная кабина", BodyType.PickupDoubleCab},
            {"универсал 5 дв.", BodyType.Wagon5Door},
            {"хэтчбек 3 дв.", BodyType.Hatchback3Door},
            {"минивэн", BodyType.Minivan},
            {"пикап полуторная кабина", BodyType.PickupExtendedCab},
            {"фургон", BodyType.Van},
            {"пикап одинарная кабина", BodyType.PickupSingleCab},
            {"седан 2 дв.", BodyType.Sedan2Door},
            {"внедорожник 3 дв.", BodyType.SUV3Door},
            {"купе-хардтоп", BodyType.CoupeHardtop},
            {"универсал 3 дв.", BodyType.Wagon3Door},
            {"внедорожник открытый", BodyType.OpenSUV},
            {"фастбек", BodyType.Fastback},
            {"микровэн", BodyType.MicroVan},
            {"спидстер", BodyType.Speedster},
            {"тарга", BodyType.Targa},
            {"лимузин", BodyType.Limousine},
            {"хэтчбек 4 дв.", BodyType.Hatchback4Door},
            {"седан-хардтоп", BodyType.SedanHardtop},
            {"фаэтон", BodyType.Phaeton},
            {"универсал", BodyType.Wagon},
            {"фаэтон-универсал", BodyType.PhaetonWagon}
        };

        return bodyTypeMapping.GetValueOrDefault(bodyType, BodyType.None);
    }
    
    public static EngineType MapEngineType(string? engineType)
    {
        if (string.IsNullOrEmpty(engineType))
            return EngineType.NoInfo;

        var engineTypeMapping = new Dictionary<string, EngineType>(StringComparer.OrdinalIgnoreCase)
        {
            {"гидроген", EngineType.Hydrogene},
            {"электро", EngineType.Electric},
            {"гибрид", EngineType.Hybrid},
            {"дизель", EngineType.Diesel},
            {"СУГ", EngineType.NaturalGas},
            {"бензин", EngineType.Petrol},
        };

        return engineTypeMapping.GetValueOrDefault(engineType, EngineType.NoInfo);
    }
    
    public static TransmissionType MapTransmissionType(string? transmissionType)
    {
        if (string.IsNullOrEmpty(transmissionType))
            return TransmissionType.NoInfo;

        var transmissionTypeMapping = new Dictionary<string, TransmissionType>(StringComparer.OrdinalIgnoreCase)
        {
            {"механическая", TransmissionType.Mechanical},
            {"автоматическая", TransmissionType.Auto},
            {"робот", TransmissionType.Robot},
            {"вариатор", TransmissionType.Variator},
        };

        return transmissionTypeMapping.GetValueOrDefault(transmissionType, TransmissionType.NoInfo);
    }
    
    public static DriveType MapDriveType(string? driveType)
    {
        if (string.IsNullOrEmpty(driveType))
            return DriveType.NoInfo;

        var driveTypeMapping = new Dictionary<string, DriveType>(StringComparer.OrdinalIgnoreCase)
        {
            {"передний", DriveType.Front},
            {"автоматическая", DriveType.Rear},
            {"робот", DriveType.Full}
        };

        return driveTypeMapping.GetValueOrDefault(driveType, DriveType.NoInfo);
    }
}