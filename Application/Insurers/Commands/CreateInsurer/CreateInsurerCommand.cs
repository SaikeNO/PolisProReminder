using MediatR;

namespace PolisProReminder.Application.Insurers.Commands.CreateInsurer;

public class CreateInsurerCommand : IRequest<Guid>
{
    public string Pesel { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
