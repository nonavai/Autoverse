namespace Application.Dto_s.Models.HelpModels;

public interface IModification
{
    public string Id { get; set; }
    public float? OffersPriceFrom { get; set; } 
    public float? OffersPriceTo { get; set; } 
    public string? GroupName { get; set; } 
    public string CarConfigurationId { get; set; }
}