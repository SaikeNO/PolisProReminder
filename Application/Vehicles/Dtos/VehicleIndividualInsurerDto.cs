namespace PolisProReminder.Application.Vehicles.Dtos;
public record VehicleIndividualInsurerDto(Guid Id, string Pesel, string FirstName, string? LastName, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseVehicleInsurerDto(Id, PhoneNumber, Email, PostalCode, City, Street)
{
    public string Pesel { get; init; } = Pesel;
    public string FirstName { get; init; } = FirstName;
    public string? LastName { get; init; } = LastName;
}
