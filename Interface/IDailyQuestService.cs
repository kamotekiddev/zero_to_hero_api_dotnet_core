using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IDailyQuestService
{
    Task<IEnumerable<DailyQuestDto>> GetAllDailyQuestAsync();
    Task<DailyQuestDto> GetDailyQuestByIdAsync(string dailyQuestId);
    Task<DailyQuestDto> CreateDailyQuest(CreateDailyQuestDto dto);
    Task<DailyQuestDto> UpdateDailyQuestAsync(string dailyQuestId, UpdateDailyQuestDto dto);
    Task<DailyQuestDto> AssignQuestToUser(string dailyQuestId, AssignDailyQuestDto dto);
}