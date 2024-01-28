using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models.InsuranceCompany
{
    public class CreateInsuranceCompanyDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; } = null!;
    }
}
