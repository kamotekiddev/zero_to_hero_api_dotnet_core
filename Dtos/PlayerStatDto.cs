namespace ZeroToHeroAPI.Dtos;

public class PlayerStatDto
{
    public string Id { get; set; }
    public int CurrentLevel { get; set; } = 1;
    public int MaxLevel { get; set; } = 100;
    public int CurrentExp { get; set; } = 0;
    public int NextLevelExp { get; set; } = 100;
    public string UserId { get; set; }
}