using Microsoft.AspNetCore.Identity;

namespace PolisProReminder.Domain.Entities;
public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string? SuperiorId { get; set; }
    public virtual User? Superior { get; set; }
}