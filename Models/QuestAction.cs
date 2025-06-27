using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Models;

public class QuestAction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string QuestTemplateId { get; set; }
    public QuestActionTypeEnum ActionType { get; set; }
    public int TargetValue { get; set; }
    public QuestActionUnitEnum Unit { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null!;
}