using System.ComponentModel.DataAnnotations;
using ZeroToHeroAPI.Attributes;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Dtos;

public class QuestRewardDto
{
    public string Id { get; set; }
    public string QuestTemplateId { get; set; }
    public QuestRewardTypeEnum RewardType { get; set; }
    public int Value { get; set; }
}

public class CreateQuestRewardDto
{
    [Required] public string QuestTemplateId { get; set; }

    [Required]
    [ValidEnum(typeof(QuestRewardTypeEnum))]
    public QuestRewardTypeEnum RewardType { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Value { get; set; }
}

public class UpdateQuestRewardDto
{
    [Required] public string QuestTemplateId { get; set; }

    [Required]
    [ValidEnum(typeof(QuestRewardTypeEnum))]
    public QuestRewardTypeEnum RewardType { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Value { get; set; }
}