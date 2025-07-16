using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroAPI.Dtos;

public class RegisterDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must be at least 6 characters long, contain letters and numbers.")]
    public string Password { get; set; }
}