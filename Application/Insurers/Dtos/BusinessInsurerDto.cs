namespace PolisProReminder.Application.Insurers.Dtos;

public record BusinessInsurerDto(Guid Id, string Name, string? Nip, string? Regon, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street, IEnumerable<InsurerPolicyDto> Policies)
: BaseInsurerDto(Id, PhoneNumber, Email, PostalCode, City, Street, Policies)
{
    public string Name { get; set; } = Name;
    public string? Nip { get; set; } = Nip;
    public string? Regon { get; set; } = Regon;
}