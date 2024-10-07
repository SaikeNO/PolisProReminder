namespace PolisProReminder.Application.Vehicles.Dtos;

public abstract record BaseVehicleInsurerDto(Guid Id, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
{
    public Guid Id { get; set; } = Id;
    public string? PhoneNumber { get; init; } = PhoneNumber;
    public string? Email { get; init; } = Email;
    public string? PostalCode { get; init; } = PostalCode;
    public string? City { get; init; } = City;
    public string? Street { get; init; } = Street;
}
