namespace Domain.Entities.CustomEntities;

public class MarkView: BaseGuidEntity
{
    public string MarkId { get; set; }
    public int Views { get; set; }
    public DateTimeOffset Date { get; set; }
    public Mark Mark { get; set; }
}