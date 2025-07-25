using System.ComponentModel.DataAnnotations;
using ZeroToHeroAPI.Attributes;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Dtos;

public class QuestTemplateDto
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public QuestDifficultyEnum Difficulty { get; set; }
    public bool IsActive { get; set; }


    public IEnumerable<QuestActionDto>? Actions { get; set; }
    public IEnumerable<QuestRewardDto> Rewards { get; set; }
    public IEnumerable<QuestPunishmentDto>? Punishments { get; set; }
}

public class CreateQuestTemplateDto
{
    [Required]
    [MinLength(5, ErrorMessage = "This should be atleast 5 characters long.")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [ValidEnum(typeof(QuestDifficultyEnum))]
    public QuestDifficultyEnum Difficulty { get; set; }
}

public class UpdateQuestTemplateDto
{
    [Required]
    [MinLength(5, ErrorMessage = "This should be atleast 5 characters long.")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [ValidEnum(typeof(QuestDifficultyEnum))]
    public QuestDifficultyEnum Difficulty { get; set; }
}

public record GetAllQuestQueryParams
{
    public bool? IsActive;
}