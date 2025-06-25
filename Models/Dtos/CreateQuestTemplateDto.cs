using System.ComponentModel.DataAnnotations;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Models.Dtos;

public class CreateQuestTemplateDto
{
    [Required]
    [MinLength(5, ErrorMessage = "This should be atleast 5 characters long.")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(QuestDifficulty))]
    public int Difficulty { get; set; }
}