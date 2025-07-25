using Microsoft.Build.Framework;
using ZeroToHeroAPI.Enums;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Dtos;

public class DailyQuestDto
{
    public string Id { get; set; }

    public DateTime? DateAssigned { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? DateCompleted { get; set; }
    public string QuestStatus { get; set; } = nameof(DailyQuestStatusEnum.Pending);

    public string PlayerId { get; set; } = string.Empty;
    public string QuestTemplateId { get; set; }

    public Player Player { get; set; }
    public QuestTemplateDto? QuestTemplate { get; set; }
    public List<QuestActionProgressDto>? QuestActionProgress { get; set; }
}

public class CreateDailyQuestDto
{
    [Required] public string QuestTemplateId { get; set; } = string.Empty;
}

public class UpdateDailyQuestDto
{
    [Required] public string QuestTemplateId { get; set; } = string.Empty;
}

public class AssignDailyQuestDto
{
    [Required] public string PlayerId { get; set; } = string.Empty;
}

public record GetAllDailyQuestQueryParams
{
    public bool? IsCompleted { get; set; }
}