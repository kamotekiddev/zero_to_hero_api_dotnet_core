using Microsoft.AspNetCore.Identity;

namespace ZeroToHeroAPI.Models;

public class User : IdentityUser
{
    public Player Player { get; set; }
}