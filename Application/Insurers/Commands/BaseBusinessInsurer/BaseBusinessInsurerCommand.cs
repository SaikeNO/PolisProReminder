using PolisProReminder.Application.Insurers.Commands.BaseInsurer;

namespace PolisProReminder.Application.Insurers.Commands.BaseBusinessInsurer;

public record BaseBusinessInsurerCommand(string? Nip, string? Regon, string Name, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseInsurerCommand(PhoneNumber, Email, PostalCode, City, Street)
{
    public string? Nip { get; init; } = Nip;
    public string? Regon { get; init; } = Regon;
    public string Name { get; init; } = Name;
}
