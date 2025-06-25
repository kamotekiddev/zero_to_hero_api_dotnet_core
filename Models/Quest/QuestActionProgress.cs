using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroToHeroAPI.Models.Quest;

public class QuestActionProgress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string DailyQuestId { get; set; }
    public string QuestActionId { get; set; }
    public int ProgressValue { get; set; } = 0;
    public bool IsActionCompleted { get; set; } = false;

    public DailyQuest DailyQuest { get; set; } = null!;
    public QuestAction QuestAction { get; set; } = null!;
}