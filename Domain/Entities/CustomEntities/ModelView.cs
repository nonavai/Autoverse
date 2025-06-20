namespace Domain.Entities.CustomEntities;

public class ModelView : BaseGuidEntity
{
    public string ModelId { get; set; }
    public int Views { get; set; }
    public DateTimeOffset Date { get; set; }
    public Model Model { get; set; }
}