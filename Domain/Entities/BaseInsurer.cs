using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

[Table("Insurers")]
public abstract record BaseInsurer(string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street) : ICreatedBy, ISoftDeletable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [ProtectedPersonalData]
    public string? PhoneNumber { get; private set; } = PhoneNumber;

    [ProtectedPersonalData]
    public string? Email { get; private set; } = Email;

    [ProtectedPersonalData]
    public string? PostalCode { get; private set; } = PostalCode;

    [ProtectedPersonalData]
    public string? City { get; private set; } = City;

    [ProtectedPersonalData]
    public string? Street { get; private set; } = Street;

    public Guid CreatedByUserId { get; set; }

    public Guid CreatedByAgentId { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual IEnumerable<Policy> Policies { get; set; } = null!;

    public virtual IEnumerable<Vehicle> Vehicles { get; set; } = null!;

    public void Update(string? phoneNumber, string? email, string? postalCode, string? city, string? street)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        PostalCode = postalCode;
        City = city;
        Street = street;
    }

    public void Delete() => IsDeleted = true;
}