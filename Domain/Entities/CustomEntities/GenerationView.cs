namespace Domain.Entities.CustomEntities;

public class GenerationView : BaseGuidEntity
{
    public string GenerationId { get; set; }
    public int Views { get; set; }
    public DateTimeOffset Date { get; set; }
    public Generation Generation { get; set; }
}