using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
