namespace PolisProReminder.Application.Insurers.Commands.BaseInsurer;

public abstract record BaseInsurerCommand(string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
{
    public string? PhoneNumber { get; init; } = PhoneNumber;
    public string? Email { get; init; } = Email;
    public string? PostalCode { get; init; } = PostalCode;
    public string? City { get; init; } = City;
    public string? Street { get; init; } = Street;
}

public record BussinesInsurerCommand(string Name, string? Nip, string? Regon, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseInsurerCommand(PhoneNumber, Email, PostalCode, City, Street)
{
    public string Name { get; init; } = Name;
    public string? Nip { get; init; } = Nip;
    public string? Regon { get; init; } = Regon;
}

