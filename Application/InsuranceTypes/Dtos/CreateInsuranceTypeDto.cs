using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Application.InsuranceTypes.Dtos;

public class CreateInsuranceTypeDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;
}
