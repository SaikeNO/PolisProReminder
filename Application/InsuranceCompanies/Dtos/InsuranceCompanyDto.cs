namespace PolisProReminder.Application.InsuranceCompanies.Dtos;

public class InsuranceCompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = string.Empty;

}
