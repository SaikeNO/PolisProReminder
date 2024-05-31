namespace PolisProReminder.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Expires { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

}
