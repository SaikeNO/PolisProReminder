namespace PolisProReminder.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public RefreshToken? RefreshToken { get; set; }

    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    public Guid? SuperiorId { get; set; }
    public virtual User? Superior { get; set; }
}