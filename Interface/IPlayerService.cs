using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IPlayerService
{
    Task<DailyQuestDto> GetPlayerQuestAsync();

    Task<QuestActionProgressDto>
        StartActionAsync(string dailyQuestId, string actionId, QuestActionProgressStartDto dto);
}