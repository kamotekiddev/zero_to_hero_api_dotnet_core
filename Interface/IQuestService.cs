using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IQuestService
{
    Task<QuestTemplateDto> CreateQuestTemplateAsync(CreateQuestTemplateDto dto);
    Task<QuestTemplateDto> UpdateQuestTemplateAsync(string id, UpdateQuestTemplateDto dto);
    Task<QuestTemplateDto> DeleteQuestTemplateAsync(string id);
    Task<List<QuestTemplateDto>> GetAllQuestTemplatesAsync();
    Task<QuestTemplateDto> GetQuestTemplateByIdAsync(string id);
}