namespace Domain.Contracts.CleanModels;

public class ModificationClean
{
    public float? OffersPriceFrom { get; set; } 
    public float? OffersPriceTo { get; set; } 
    public string? GroupName { get; set; } 
    public string CarConfigurationId { get; set; }
}