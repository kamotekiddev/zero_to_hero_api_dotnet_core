using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroAPI.Dtos;

public class RefreshTokenDto
{
    [Required] public string RefreshToken { get; set; }
}