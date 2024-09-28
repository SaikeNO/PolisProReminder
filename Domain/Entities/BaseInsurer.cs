using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

public abstract record BaseInsurer(string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street) : ICreatedBy, ISoftDeletable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? PhoneNumber { get; private set; } = PhoneNumber;
    public string? Email { get; private set; } = Email;
    public string? PostalCode { get; private set; } = PostalCode;
    public string? City { get; private set; } = City;
    public string? Street { get; private set; } = Street;

    public Guid CreatedByUserId { get; set; }
    public Guid CreatedByAgentId { get; set; }

    public bool IsDeleted { get; set; } = false;
    public virtual List<Policy> Policies { get; set; } = [];
    public virtual List<Vehicle> Vehicle { get; set; } = [];

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