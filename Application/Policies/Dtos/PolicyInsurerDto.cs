namespace PolisProReminder.Application.Policies.Dtos;

public record PolicyInsurerDto(Guid Id, string Pesel, string FirstName, string? LastName, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
{
    public Guid Id { get; set; } = Id;
    public string Pesel { get; init; } = Pesel;
    public string FirstName { get; init; } = FirstName;
    public string? LastName { get; init; } = LastName;
    public string? PhoneNumber { get; init; } = PhoneNumber;
    public string? Email { get; init; } = Email;
    public string? PostalCode { get; init; } = PostalCode;
    public string? City { get; init; } = City;
    public string? Street { get; init; } = Street;
}
