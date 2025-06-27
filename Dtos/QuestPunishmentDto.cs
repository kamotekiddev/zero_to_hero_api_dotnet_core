using System.ComponentModel.DataAnnotations;
using ZeroToHeroAPI.Attributes;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Dtos;

public class QuestPunishmentDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public QuestPunishmentTypeEnum PunishmentTypeEnum { get; set; }
    public int Value { get; set; }
}

public class CreateQuestPunishmentDto
{
    [Required] public string QuestTemplateId { get; set; }

    [Required]
    [ValidEnum(typeof(QuestPunishmentTypeEnum))]
    public QuestPunishmentTypeEnum PunishmentType { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Value { get; set; }
}

public class UpdateQuestPunishmentDto
{
    [Required] public string QuestTemplateId { get; set; }

    [Required]
    [ValidEnum(typeof(QuestPunishmentTypeEnum))]
    public QuestPunishmentTypeEnum PunishmentType { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Value { get; set; }
}