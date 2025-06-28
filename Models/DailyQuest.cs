using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Models;

public class DailyQuest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public DateTime? DateAssigned { get; set; } = null;
    public bool IsCompleted { get; set; } = false;
    public DateTime? DateCompleted { get; set; }
    public string QuestStatus { get; set; } = nameof(DailyQuestStatusEnum.Pending);

    public string UserId { get; set; } = string.Empty;
    public string QuestTemplateId { get; set; }

    public QuestTemplate QuestTemplate { get; set; } = null;
    public List<QuestActionProgress> ActionProgresses { get; set; } = new();
}