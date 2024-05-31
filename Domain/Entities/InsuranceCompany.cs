namespace PolisProReminder.Domain.Entities;

public class InsuranceCompany
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = string.Empty;
    public virtual List<Policy> Policies { get; set; } = [];
}
