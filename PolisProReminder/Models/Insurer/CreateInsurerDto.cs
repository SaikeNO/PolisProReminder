using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models.Insurer
{
    public class CreateInsurerDto
    {
        [Required]
        [RegularExpression(Recources.PeselRegex)]
        public string Pesel { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [MaxLength(20)]
        public string? LastName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
