using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Interface;

public interface IDailyQuestService
{
    Task<IEnumerable<DailyQuestDto>> GetAllDailyQuestAsync(GetAllDailyQuestQueryParams queryParams);
    Task<DailyQuestDto> GetDailyQuestByIdAsync(string dailyQuestId);
    Task<DailyQuestDto> CreateDailyQuest(CreateDailyQuestDto dto);
    Task<DailyQuestDto> UpdateDailyQuestAsync(string dailyQuestId, UpdateDailyQuestDto dto);
    Task<DailyQuestDto> AssignQuestToUser(string dailyQuestId, AssignDailyQuestDto dto);
    Task<bool> CreateAndAssignDailyQuestToUsers(List<DailyQuest> dailyQuests);
    Task<List<DailyQuestDto>> GetFailingQuest();
    Task<DailyQuestDto> QuestFailedAsync(string dailyQuestId);
}