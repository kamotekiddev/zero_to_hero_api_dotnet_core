using ZeroToHeroAPI.Enums;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Dtos;

public class PlayerHistoryDto
{
    public string Id { get; set; }

    public int OldValue { get; set; }
    public int NewValue { get; set; }
    public PlayerActionEnum Action { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string PlayerId { get; set; }
}