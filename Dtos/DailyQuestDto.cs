using Microsoft.Build.Framework;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Dtos;

public class DailyQuestDto
{
    public string Id { get; set; }

    public DateTime DateAssigned { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? DateCompleted { get; set; }
    public string QuestStatus { get; set; } = nameof(DailyQuestStatusEnum.Pending);

    public string UserId { get; set; } = string.Empty;
    public string QuestTemplateId { get; set; }

    public IEnumerable<QuestTemplateDto>? QuestTemplate { get; set; }
}

public class CreateDailyQuestDto
{
    [Required] public string QuestTemplateId { get; set; } = string.Empty;
    [Required] public string UserId { get; set; } = string.Empty;
}

public class UpdateDailyQuestDto
{
    [Required] public string QuestTemplateId { get; set; } = string.Empty;
    [Required] public string UserId { get; set; } = string.Empty;
}