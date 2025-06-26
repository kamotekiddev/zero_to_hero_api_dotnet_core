using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IDailyQuestService
{
    Task<QuestTemplateDto> GetQuestTemplates();
    Task<QuestTemplateDto> GetQuestTemplateById(string id);
    Task<QuestTemplateDto> CreateQuestTemplate(QuestTemplateDto questTemplateDto);
}