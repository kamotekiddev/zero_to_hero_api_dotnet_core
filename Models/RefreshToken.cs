using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroToHeroAPI.Models;

public class RefreshToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string UserId { get; set; }

    public User User { get; set; }
}