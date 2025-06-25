namespace ZeroToHeroAPI.Models.Quest;

public class QuestAction
{
    public int Id { get; set; }
    public int QuestTemplateId { get; set; }
    public string ActionType { get; set; } = string.Empty; // e.g. Walk, Pushup
    public int TargetValue { get; set; }
    public string Unit { get; set; } = string.Empty;

    public QuestTemplate QuestTemplate { get; set; } = null!;
}