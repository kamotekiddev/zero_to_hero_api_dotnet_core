namespace ZeroToHeroAPI.Dtos;

public class QuestActionDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public int TargetValue { get; set; }
    public string Unit { get; set; } = string.Empty;
}

public class CreateQuestActionDto
{
    public string QuestTemplateId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public int TargetValue { get; set; }
    public string Unit { get; set; } = string.Empty;
}

public class UpdateQuestActionDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public int TargetValue { get; set; }
    public string Unit { get; set; } = string.Empty;
}