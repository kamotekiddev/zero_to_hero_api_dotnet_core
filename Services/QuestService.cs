using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models.Dtos;
using ZeroToHeroAPI.Models.Quest;

namespace ZeroToHeroAPI.Services;

public class QuestService : IQuestService
{
    private readonly ApplicationDbContext _context;

    public QuestService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuestTemplateDto> CreateQuestTemplateAsync(CreateQuestTemplateDto dto)
    {
        var entity = new QuestTemplate()
        {
            Title = dto.Title,
            Description = dto.Description,
            Difficulty = dto.Difficulty
        };

        _context.QuestTemplates.Add(entity);
        await _context.SaveChangesAsync();

        return new QuestTemplateDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Difficulty = entity.Difficulty
        };
    }

    public async Task<QuestTemplateDto> UpdateQuestTemplateAsync(string id, UpdateQuestTemplateDto dto)
    {
        var existingQuest = await _context.QuestTemplates.FindAsync(id);
        if (existingQuest == null) throw new InvalidOperationException("Quest does not exist.");

        var entity = new QuestTemplate()
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            Difficulty = dto.Difficulty
        };

        _context.QuestTemplates.Update(entity);
        await _context.SaveChangesAsync();

        return new QuestTemplateDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Difficulty = entity.Difficulty
        };
    }

    public Task<QuestTemplateDto> DeleteQuestTemplateAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<QuestTemplateDto>> GetAllQuestTemplatesAsync()
    {
        var rawQuests = await _context.QuestTemplates.ToListAsync();

        return rawQuests.Select(quest => new QuestTemplateDto()
        {
            Id = quest.Id,
            Title = quest.Title,
            Description = quest.Description,
            Difficulty = quest.Difficulty
        }).ToList();
    }

    public Task<QuestTemplateDto> GetQuestTemplateByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}