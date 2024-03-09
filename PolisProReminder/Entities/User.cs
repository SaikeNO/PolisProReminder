namespace PolisProReminder.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public RefreshToken? RefreshToken { get; set; }

    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    public int? SuperiorId { get; set; }
    public virtual User? Superior { get; set; }
}