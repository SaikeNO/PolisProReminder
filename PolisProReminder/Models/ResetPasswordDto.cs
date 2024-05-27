using System.ComponentModel.DataAnnotations;

namespace PolisProReminder.Models;

public class ResetPasswordDto
{
    [Required]
    public string OldPassword { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = null!;
}
