namespace PolisProReminder.Application.Users.Dtos;

public record UserDto : BaseUserDto
{
    public bool IsEmailConfirmed { get; init; }
    public bool IsLockedOut { get; init; }
    public IEnumerable<string> Roles { get; init; } = default!;

}
