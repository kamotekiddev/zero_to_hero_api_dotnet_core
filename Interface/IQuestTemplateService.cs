using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IQuestTemplateService
{
    Task<QuestTemplateDto> CreateQuestTemplateAsync(CreateQuestTemplateDto dto);
    Task<QuestTemplateDto> UpdateQuestTemplateAsync(string id, UpdateQuestTemplateDto dto);
    Task<QuestTemplateDto> DeleteQuestTemplateAsync(string id);
    Task<List<QuestTemplateDto>> GetAllQuestTemplatesAsync(GetAllQuestQueryParams queryParams);
    Task<QuestTemplateDto> GetQuestTemplateByIdAsync(string id);
}