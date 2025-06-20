
using Application.Dto_s.Models.HelpModels;

namespace Application.Dto_s.Models;

public class ModificationSimple : IModification
{
    public Engine Engine { get; set; }
    public Mobility Mobility { get; set; }
    public string Id { get; set; }
    public float? OffersPriceFrom { get; set; }
    public float? OffersPriceTo { get; set; }
    public string? GroupName { get; set; }
    public string CarConfigurationId { get; set; }
}