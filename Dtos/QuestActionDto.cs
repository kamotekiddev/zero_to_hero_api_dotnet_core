using System.ComponentModel.DataAnnotations;
using ZeroToHeroAPI.Attributes;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Dtos;

public class QuestActionDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public QuestActionTypeEnum ActionType { get; set; }
    public int TargetValue { get; set; }
    public QuestActionUnitEnum Unit { get; set; }
}

public class CreateQuestActionDto
{
    [Required] public string QuestTemplateId { get; set; }

    [Required]
    [ValidEnum(typeof(QuestActionTypeEnum))]
    public QuestActionTypeEnum ActionType { get; set; }

    [Required] [Range(1, int.MaxValue)] public int TargetValue { get; set; }


    [Required]
    [ValidEnum(typeof(QuestActionUnitEnum))]
    public QuestActionUnitEnum Unit { get; set; }
}

public class UpdateQuestActionDto
{
    [Required] public string QuestTemplateId { get; set; }

    [Required]
    [ValidEnum(typeof(QuestActionTypeEnum))]
    public QuestActionTypeEnum ActionType { get; set; }


    [Required] [Range(1, int.MaxValue)] public int TargetValue { get; set; }

    [Required]
    [ValidEnum(typeof(QuestActionUnitEnum))]
    public QuestActionUnitEnum Unit { get; set; }
}