using System.ComponentModel;
using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public enum PlayerAction
{
    ExpGained,
    ExpDecreased,
    LeveledUp,
    LeveledDown,
}

public interface IPlayerStatService
{
    Task<PlayerStatDto> InitializePlayerStatAsync(
        string userId);

    Task<(PlayerStatDto playerStat, List<PlayerAction> actions)>
        UpdatePlayerStatAsync(string playerStatId, UpdatePlayerStatsDto updatePlayerStatsDto);
}