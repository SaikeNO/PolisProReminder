using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models
{
    public class CreateInsuranceTypeDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
    }
}
