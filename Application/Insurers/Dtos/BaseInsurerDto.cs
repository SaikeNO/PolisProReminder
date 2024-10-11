using PolisProReminder.Application.Policies.Dtos;

namespace PolisProReminder.Application.Insurers.Dtos;

public abstract record BaseInsurerDto(Guid Id, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street, IEnumerable<InsurerPolicyDto> Policies)
{
    public Guid Id { get; set; } = Id;
    public string? PhoneNumber { get; init; } = PhoneNumber;
    public string? Email { get; init; } = Email;
    public string? PostalCode { get; init; } = PostalCode;
    public string? City { get; init; } = City;
    public string? Street { get; init; } = Street;
    public IEnumerable<InsurerPolicyDto> Policies { get; set; } = Policies;
}
