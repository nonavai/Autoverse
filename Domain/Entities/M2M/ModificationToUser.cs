namespace Domain.Entities.M2M;

public class ModificationToUser
{
    public Guid UserId { get; set; }
    public string ModifiactionId { get; set; }
    public User User { get; set; }
    public Modification Modification { get; set; }
    public DateTimeOffset VisitDate { get; set; }
}