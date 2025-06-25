namespace ZeroToHeroAPI.Models.Quest;

public class QuestActionProgress
{
    public int Id { get; set; }
    public int UserDailyQuestId { get; set; }
    public int QuestActionId { get; set; }
    public int ProgressValue { get; set; } = 0;
    public bool IsActionCompleted { get; set; } = false;

    public DailyQuest DailyQuest { get; set; } = null!;
    public QuestAction QuestAction { get; set; } = null!;
}