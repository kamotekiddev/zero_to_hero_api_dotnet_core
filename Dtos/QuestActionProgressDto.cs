using Microsoft.Build.Framework;

namespace ZeroToHeroAPI.Dtos;

public class QuestActionProgressDto
{
    public string Id { get; set; }

    public string DailyQuestId { get; set; }
    public string QuestActionId { get; set; }
    public int ProgressValue { get; set; } = 0;
    public bool IsActionCompleted { get; set; } = false;
}

public class QuestActionProgressStartDto
{
    [Required] public int ProgressValue { get; set; } = 0;
}