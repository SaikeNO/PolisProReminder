using PolisProReminder.Entities;

namespace PolisProReminder.Models;

public class UserDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;

    public User? Superior { get; set; }
}
