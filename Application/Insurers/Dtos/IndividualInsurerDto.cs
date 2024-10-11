using PolisProReminder.Application.Policies.Dtos;

namespace PolisProReminder.Application.Insurers.Dtos;

public record IndividualInsurerDto(Guid Id, string Pesel, string FirstName, string? LastName, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street, IEnumerable<InsurerPolicyDto> Policies)
: BaseInsurerDto(Id, PhoneNumber, Email, PostalCode, City, Street, Policies)
{
    public string Pesel { get; init; } = Pesel;
    public string FirstName { get; init; } = FirstName;
    public string? LastName { get; init; } = LastName;
}
