using PolisProReminder.Entities;
using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models
{
    public class CreatePolicyDto
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string PolicyNumber { get; set; } = null!;

        [Required]
        public InsuranceCompanyDto InsuranceCompany { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }

        public PolicyInsurerDto Insurer { get; set; } = null!;
        public List<InsuranceTypeDto> InsuranceTypes { get; set; } = null!;
    }
}
