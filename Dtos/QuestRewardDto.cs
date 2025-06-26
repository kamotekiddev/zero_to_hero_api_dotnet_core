namespace ZeroToHeroAPI.Dtos;

public class QuestRewardDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public string RewardType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}

public class CreateQuestRewardDto
{
    public string QuestTemplateId { get; set; }
    public string RewardType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}

public class UpdateQuestRewardDto
{
    public string QuestTemplateId { get; set; }
    public string RewardType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}