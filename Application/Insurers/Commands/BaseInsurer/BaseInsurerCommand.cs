namespace PolisProReminder.Application.Insurers.Commands.BaseInsurer;

public record BaseInsurerCommand(string Pesel, string FirstName, string? LastName, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
{
    public string Pesel { get; init; } = Pesel;
    public string FirstName { get; init; } = FirstName;
    public string? LastName { get; init; } = LastName;
    public string? PhoneNumber { get; init; } = PhoneNumber;
    public string? Email { get; init; } = Email;
    public string? PostalCode { get; init; } = PostalCode;
    public string? City { get; init; } = City;
    public string? Street { get; init; } = Street;
}
