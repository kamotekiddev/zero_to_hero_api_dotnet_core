namespace ZeroToHeroAPI.Models.Quest;

public class QuestReward
{
    public int Id { get; set; }
    public int QuestTemplateId { get; set; }
    public string RewardType { get; set; } = string.Empty; // e.g. EXP, Coin
    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null!;
}