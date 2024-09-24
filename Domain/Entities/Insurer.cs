namespace PolisProReminder.Domain.Entities;
public class Insurer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Pesel { get; set; } = null!;
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string CreatedByUserId { get; set; } = null!;
    public string CreatedByAgentId { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
    public virtual List<Policy> Policies { get; set; } = null!;
    public virtual List<Vehicle> Vehicle { get; set; } = [];
}
