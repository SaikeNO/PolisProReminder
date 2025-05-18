namespace PolisProReminder.Application.Users.Dtos;

public record UserDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool IsEmailConfirmed { get; init; }
    public bool IsLockedOut { get; init; }
    public IEnumerable<string> Roles { get; init; } = default!;
}
