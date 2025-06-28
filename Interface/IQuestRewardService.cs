using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IQuestRewardService
{
    Task<QuestRewardDto> CreateQuestRewardAsync(CreateQuestRewardDto dto);
    Task<QuestRewardDto> UpdateQuestRewardAsync(string id, UpdateQuestRewardDto dto);
    Task<QuestRewardDto> DeleteQuestRewardAsync(string id);
    Task<List<QuestRewardDto>> GetAllQuestRewardsAsync();
    Task<QuestRewardDto> GetQuestRewardByIdAsync(string id);
}