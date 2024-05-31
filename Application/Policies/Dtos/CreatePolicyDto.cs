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
    public int InsuranceCompanyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaid { get; set; }

    [Required]
    public int InsurerId { get; set; }
    public int[] InsuranceTypeIds { get; set; } = [];
}
