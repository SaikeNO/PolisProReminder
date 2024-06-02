using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Application.InsuranceTypes.Dtos;

namespace PolisProReminder.Application.Policies.Dtos;

public class PolicyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public InsuranceCompanyDto InsuranceCompany { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaid { get; set; }

    public PolicyInsurerDto Insurer { get; set; } = null!;
    public List<InsuranceTypeDto> InsuranceTypes { get; set; } = new();
}
