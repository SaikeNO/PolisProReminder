namespace PolisProReminder.Application.Insurers.Dtos;

public record VehicleBusinessInsurerDto(Guid Id, string Name, string? Nip, string? Regon, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseVehicleInsurerDto(Id, PhoneNumber, Email, PostalCode, City, Street)
{
    public string Name { get; set; } = Name;
    public string? Nip { get; set; } = Nip;
    public string? Regon { get; set; } = Regon;
}
