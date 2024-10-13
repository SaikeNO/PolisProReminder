using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Application.InsuranceTypes.Dtos;

namespace PolisProReminder.Application.Policies.Dtos;

public class InsurerPolicyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public InsuranceCompanyDto InsuranceCompany { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly PaymentDate { get; set; }
    public bool IsPaid { get; set; }

    public IEnumerable<InsuranceTypeDto> InsuranceTypes { get; set; } = [];
}
