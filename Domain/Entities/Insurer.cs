namespace PolisProReminder.Domain.Entities;
public abstract record Insurer(string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? PhoneNumber { get; private set; } = PhoneNumber;
    public string? Email { get; private set; } = Email;
    public string? PostalCode { get; private set; } = PostalCode;
    public string? City { get; private set; } = City; 
    public string? Street { get; private set; } = Street;
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;
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
}

public record BussinesInsurer(string Name, string? Nip, string? Regon, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street) 
    : Insurer(PhoneNumber,  Email,  PostalCode,  City, Street)
{
    public string Name { get; private set; } = Name;
    public string? Nip { get; private set; } = Nip;
    public string? Regon { get; private set; } = Regon;

    public void Update(string name, string? nip, string regon)
    {
        Name = name;
        Nip = nip;
        Regon = regon;
    }
}

public record IndividualInsurer(string FirstName, string? LastName, string? Pesel, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : Insurer(PhoneNumber, Email, PostalCode, City, Street)
{
    public string FirstName { get; private set; } = FirstName;
    public string? LastName { get; private set; } = LastName;
    public string? Pesel { get; private set; } = Pesel;

    public void Update(string firstName, string? lastName, string? pesel)
    {
        FirstName = firstName;
        LastName = lastName;
        Pesel = pesel;
    }
}
