using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IQuestActionService
{
    Task<QuestActionDto> CreateQuestActionAsync(CreateQuestActionDto dto);
    Task<QuestActionDto> UpdateQuestActionAsync(string id, UpdateQuestActionDto dto);
    Task<QuestActionDto> DeleteQuestActionAsync(string id);
    Task<List<QuestActionDto>> GetAllQuestActionsAsync();
    Task<QuestActionDto> GetQuestActionByIdAsync(string id);
}