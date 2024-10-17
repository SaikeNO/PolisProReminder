using PolisProReminder.Application.InsuranceCompanies.Dtos;
using PolisProReminder.Application.InsuranceTypes.Dtos;
using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Policies.Dtos;

public record PolicyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public InsuranceCompanyDto InsuranceCompany { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public string? Note { get; set; }

    public IEnumerable<InsurerBasicInfoDto> Insurers { get; set; } = null!;
    public IEnumerable<InsuranceTypeDto> InsuranceTypes { get; set; } = null!;
}
