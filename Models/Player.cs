using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroAPI.Models;

public class Player
{
    [Key] public string Id { get; set; }
    public int CurrentLevel { get; set; } = 1;
    public int MaxLevel { get; set; } = 100;
    public int CurrentExp { get; set; } = 0;
    public int NextLevelExp { get; set; } = 100;

    public string UserId { get; set; }
    public User User { get; set; }

    public ICollection<PlayerHistory> History { get; set; }
}