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
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly PaymentDate { get; set; }
    public bool IsPaid { get; set; }

    [Required]
    public Guid InsurerId { get; set; }
    public List<Guid> InsuranceTypeIds { get; set; } = [];
}
