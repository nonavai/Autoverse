using Application.Dto_s.Models;
using Domain.Enums;

namespace Application.Dto_s.Responses;

public class GetRandomCarsResponse
{
    public string MarkId { get; set; }
    public string MarkName { get; set; }
    public string MarkCyrillicName { get; set; }
    public int Popular { get; set; }
    public Country Country { get; set; }
    public string ModelId { get; set; }
    public string ModelName { get; set; } 
    public string ModelCyrillicName { get; set; } 
    public Class Class { get; set; } 
    public int YearFrom { get; set; }
    public int YearTo { get; set; } 
    public string GenerationId { get; set; }
    public string GenerationName { get; set; } 
    public int YearStart { get; set; } 
    public int YearStop { get; set; } 
    public bool IsRestyle { get; set; }
    public string CarConfigurationId { get; set; }
    public int DoorsCount { get; set; } 
    public BodyType BodyType { get; set; } 
    public string? ConfigurationName { get; set; }
    public string ModificationId { get; set; }
    public float? OffersPriceFrom { get; set; } 
    public float? OffersPriceTo { get; set; } 
    public string? GroupName { get; set; } 
    public Engine Engine { get; set; }
    public Mobility Mobility { get; set; }
}