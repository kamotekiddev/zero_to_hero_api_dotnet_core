using Microsoft.AspNetCore.Identity;

namespace ZeroToHeroAPI.Models;

public class User : IdentityUser
{
    public PlayerStat PlayerStat { get; set; }
}