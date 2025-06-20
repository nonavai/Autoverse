using Application.Dto_s.Models;

namespace Application.Dto_s.Responses;

public class GetModificationInfoResponse
{
    public string Id { get; set; }
    public float? OffersPriceFrom { get; set; } 
    public float? OffersPriceTo { get; set; } 
    public string? GroupName { get; set; } 
    //public string CarConfigurationId { get; set; }
    public Comfort Comfort { get; set; }
    public Dimension Dimension { get; set; }
    public Emissions Emissions { get; set; }
    public Engine Engine { get; set; }
    public Exterior Exterior { get; set; }
    public Interior Interior { get; set; }
    public Mobility Mobility { get; set; }
    public Performance Performance { get; set; }
    public Safety Safety { get; set; }
    public Weight Weight { get; set; }
}