namespace Domain.Entities.CustomEntities;

public class CarConfigurationView : BaseGuidEntity
{
    public string CarConfigurationId { get; set; }
    public int Views { get; set; }
    public DateTimeOffset Date { get; set; }
    public CarConfiguration CarConfiguration { get; set; }
}