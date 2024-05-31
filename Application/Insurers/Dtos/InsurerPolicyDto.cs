using PolisProReminder.Application.InsuranceTypes.Dtos;

namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurerPolicyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string PolicyNumber { get; set; } = null!;
    public string InsuranceCompany { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaid { get; set; }

    public List<InsuranceTypeDto> InsuranceTypes { get; set; } = new();
}
