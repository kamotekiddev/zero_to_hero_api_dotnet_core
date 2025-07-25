using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Interface;

public interface IPlayerService
{
    Task<List<PlayerDto>> GetAllPlayersAsync();
    Task<DailyQuestDto> GetPlayerQuestAsync();

    Task<QuestActionProgressDto>
        StartActionAsync(string dailyQuestId, string actionId, QuestActionProgressStartDto dto);

    Task<PlayerDto> InitializePlayerAsync(
        string userId);

    Task<(PlayerDto player, List<PlayerActionEnum> actions)>
        UpdatePlayerAsync(string playerId, int exp);
}