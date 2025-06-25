using ZeroToHeroAPI.Models.Quest;

namespace ZeroToHeroAPI.Interface;

public interface IQuestService
{
    Task CreateQuestTemplate(QuestTemplate questTemplate);
    Task UpdateQuestTemplate(QuestTemplate questTemplate);
    Task DeleteQuestTemplate(int id);
    Task<List<QuestTemplate>> GetAllQuestTemplates();
    Task<QuestTemplate> GetQuestTemplateById(int id);
}