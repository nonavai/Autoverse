using System.Text.Json.Serialization;
using Application.Dto_s.Models.HelpModels;

namespace Application.Dto_s.Models;

public class Modification : IModification
{
    public string Id { get; set; }
    public float? OffersPriceFrom { get; set; } 
    public float? OffersPriceTo { get; set; } 
    public string? GroupName { get; set; } 
    public string CarConfigurationId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Comfort Comfort { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dimension Dimension { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Emissions Emissions { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Engine Engine { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Exterior Exterior { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Interior Interior { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Mobility Mobility { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Performance Performance { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Safety Safety { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Weight Weight { get; set; }
}