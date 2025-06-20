namespace Domain.Entities.M2M;

public class FavoriteMarkToUser
{
    public Guid UserId { get; set; }
    public string MarkId { get; set; }
    public User User { get; set; }
    public Mark Mark { get; set; }
}