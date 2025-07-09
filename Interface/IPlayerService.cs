using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IPlayerService
{
    Task<DailyQuestDto> GetPlayerQuestAsync();
}