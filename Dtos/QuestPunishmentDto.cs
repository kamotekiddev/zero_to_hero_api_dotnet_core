namespace ZeroToHeroAPI.Dtos;

public class QuestPunishmentDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public string PunishmentType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}

public class CreateQuestPunishmentDto
{
    public string QuestTemplateId { get; set; }
    public string PunishmentType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}

public class UpdateQuestPunishmentDto
{
    public string QuestTemplateId { get; set; }
    public string PunishmentType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}