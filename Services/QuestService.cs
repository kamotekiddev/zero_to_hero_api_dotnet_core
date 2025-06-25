using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models.Dtos;
using ZeroToHeroAPI.Models.Quest;

namespace ZeroToHeroAPI.Services;

public class QuestService : IQuestService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QuestService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

        return _mapper.Map<QuestTemplateDto>(entity);
    }

    public async Task<QuestTemplateDto> UpdateQuestTemplateAsync(string id, UpdateQuestTemplateDto dto)
    {
        var existingQuest = await _context.QuestTemplates.FindAsync(id);
        if (existingQuest == null) throw new KeyNotFoundException("Quest does not exist.");

        var entity = new QuestTemplate()
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            Difficulty = dto.Difficulty
        };

        _context.QuestTemplates.Update(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<QuestTemplateDto>(entity);
    }

    public async Task<QuestTemplateDto> DeleteQuestTemplateAsync(string id)
    {
        var existingQuestTemplate = await _context.QuestTemplates.FindAsync(id);
        if (existingQuestTemplate == null) throw new KeyNotFoundException("Quest template does not exist.");

        _context.Remove(existingQuestTemplate);
        await _context.SaveChangesAsync();

        return _mapper.Map<QuestTemplateDto>(existingQuestTemplate);
    }

    public async Task<List<QuestTemplateDto>> GetAllQuestTemplatesAsync()
    {
        var rawQuests = await _context.QuestTemplates.ToListAsync();
        return _mapper.Map<List<QuestTemplateDto>>(rawQuests);
    }

    public async Task<QuestTemplateDto> GetQuestTemplateByIdAsync(string id)
    {
        var quest = await _context.QuestTemplates.FindAsync(id);
        if (quest == null) throw new KeyNotFoundException("Quest not found");

        return _mapper.Map<QuestTemplateDto>(quest);
    }
}