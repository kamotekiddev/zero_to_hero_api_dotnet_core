using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IQuestPunishmentService
{
    Task<QuestPunishmentDto> CreateQuestPunishmentAsync(CreateQuestPunishmentDto dto);
    Task<QuestPunishmentDto> UpdateQuestPunishmentAsync(string id, UpdateQuestPunishmentDto dto);
    Task<QuestPunishmentDto> DeleteQuestPunishmentAsync(string id);
    Task<List<QuestPunishmentDto>> GetAllQuestPunishmentsAsync();
    Task<QuestPunishmentDto> GetQuestPunishmentByIdAsync(string id);
}