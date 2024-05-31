using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Application.Policies.Dtos;

public class CreatePolicyDto
{
    [Required]
    [MaxLength(60)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(60)]
    public string PolicyNumber { get; set; } = null!;

    [Required]
    public Guid InsuranceCompanyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaid { get; set; }

    [Required]
    public Guid InsurerId { get; set; }
    public List<Guid> InsuranceTypeIds { get; set; } = [];
}
