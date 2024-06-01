namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Pesel { get; set; }
    public virtual List<InsurerPolicyDto> Policies { get; set; } = null!;
}
