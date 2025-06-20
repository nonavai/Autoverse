namespace Domain.Entities.M2M;

public class FavoriteModificationToUser
{
    public Guid UserId { get; set; }
    public string ModificationId { get; set; }
    public User User { get; set; }
    public Modification Modification { get; set; }
    public DateTimeOffset VisitDate { get; set; }
}