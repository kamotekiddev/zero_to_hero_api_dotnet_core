namespace ZeroToHeroAPI.Dtos;

public class QuestTemplateDto
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Difficulty { get; set; }
}