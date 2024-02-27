using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models
{
    public class CreateInsuranceCompanyDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; } = null!;
    }
}
