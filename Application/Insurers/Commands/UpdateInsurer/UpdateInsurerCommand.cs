using MediatR;

namespace PolisProReminder.Application.Insurers.Commands.UpdateInurer;

public class UpdateInsurerCommand : IRequest
{
    public Guid Id { get; set; }
    public string Pesel { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
