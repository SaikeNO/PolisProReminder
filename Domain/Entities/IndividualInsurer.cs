using Microsoft.AspNetCore.Identity;

namespace PolisProReminder.Domain.Entities;

public record IndividualInsurer(string FirstName, string? LastName, string? Pesel, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseInsurer(PhoneNumber, Email, PostalCode, City, Street)
{
    [ProtectedPersonalData]
    public string FirstName { get; private set; } = FirstName;

    [ProtectedPersonalData]
    public string? LastName { get; private set; } = LastName;

    [ProtectedPersonalData]
    public string? Pesel { get; private set; } = Pesel;

    public void Update(string firstName, string? lastName, string? pesel)
    {
        FirstName = firstName;
        LastName = lastName;
        Pesel = pesel;
    }
}
