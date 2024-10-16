namespace PolisProReminder.Domain.Entities;

public record BusinessInsurer(string Name, string? Nip, string? Regon, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street) 
    : BaseInsurer(PhoneNumber,  Email,  PostalCode,  City, Street)
{
    public string Name { get; private set; } = Name;
    public string? Nip { get; private set; } = Nip;
    public string? Regon { get; private set; } = Regon;

    public void Update(string name, string? nip, string? regon)
    {
        Name = name;
        Nip = nip;
        Regon = regon;
    }
}


