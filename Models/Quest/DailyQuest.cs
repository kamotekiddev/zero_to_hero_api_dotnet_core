namespace ZeroToHeroAPI.Models.Quest;

public class DailyQuest
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int QuestTemplateId { get; set; }
    public DateTime DateAssigned { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? DateCompleted { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null!;
    public List<QuestActionProgress> ActionProgresses { get; set; } = new();
}