using ZeroToHeroAPI.Models.Dtos;
using ZeroToHeroAPI.Models.Quest;

namespace ZeroToHeroAPI.Interface;

public interface IQuestService
{
    Task<QuestTemplateDto> CreateQuestTemplateAsync(CreateQuestTemplateDto dto);
    Task<QuestTemplateDto> UpdateQuestTemplateAsync(QuestTemplateDto questTemplateDto);
    Task<QuestTemplateDto> DeleteQuestTemplateAsync(string id);
    Task<List<QuestTemplateDto>> GetAllQuestTemplatesAsync();
    Task<QuestTemplateDto> GetQuestTemplateByIdAsync(string id);
}