using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public bool IsDeleted { get; set; }
}