using Microsoft.AspNetCore.Identity;

namespace ZeroToHeroAPI.Data;

public class User : IdentityUser
{
    public PlayerStat PlayerStat { get; set; }
}