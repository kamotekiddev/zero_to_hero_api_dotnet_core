using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroToHeroAPI.Models.Quest;

public class QuestReward
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string QuestTemplateId { get; set; }
    public string RewardType { get; set; } = string.Empty; // e.g. EXP, Coin
    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null!;
}