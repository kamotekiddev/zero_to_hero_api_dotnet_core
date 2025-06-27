using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Models;

public class QuestTemplate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public QuestDifficultyEnum Difficulty { get; set; }
    public bool IsActive { get; set; } = true;

    public List<QuestAction> Actions { get; set; } = new();
    public List<QuestReward> Rewards { get; set; } = new();
    public List<QuestPunishment> Punishments { get; set; } = new();
}