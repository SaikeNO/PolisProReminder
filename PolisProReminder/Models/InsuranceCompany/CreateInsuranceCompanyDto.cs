using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models
{
    public class CreateInsuranceCompanyDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; } = null!;
        
        [MaxLength(30)]
        public string ShortName { get; set; } = string.Empty;
    }
}
