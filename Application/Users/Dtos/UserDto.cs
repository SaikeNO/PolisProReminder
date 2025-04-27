namespace PolisProReminder.Application.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public IEnumerable<string> Roles { get; set; } = default!;
}
