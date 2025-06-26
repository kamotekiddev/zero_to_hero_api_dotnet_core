using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroToHeroAPI.Models.Quest;

public class QuestPunishment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string QuestTemplateId { get; set; }
    public string PunishmentType { get; set; } = string.Empty;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null;
}