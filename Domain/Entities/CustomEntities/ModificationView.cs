namespace Domain.Entities.CustomEntities;

public class ModificationView : BaseGuidEntity
{
    public string ModificationId { get; set; }
    public int Views { get; set; }
    public DateTimeOffset Date { get; set; }
    public Modification Modification { get; set; }
}