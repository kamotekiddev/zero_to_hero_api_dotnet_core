using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroToHeroAPI.Models.Quest;

public class QuestAction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string QuestTemplateId { get; set; }
    public string ActionType { get; set; } = string.Empty; // e.g. Walk, Pushup
    public int TargetValue { get; set; }
    public string Unit { get; set; } = string.Empty;

    public QuestTemplate QuestTemplate { get; set; } = null!;
}