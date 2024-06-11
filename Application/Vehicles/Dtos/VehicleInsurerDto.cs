namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehicleInsurerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Pesel { get; set; }
}
