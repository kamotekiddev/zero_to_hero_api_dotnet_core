using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Models;

public class QuestReward
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string QuestTemplateId { get; set; }
    public QuestRewardTypeEnum RewardType { get; set; }
    public int Value { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null!;
}