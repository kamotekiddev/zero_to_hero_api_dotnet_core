using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class QuestTemplateService : IQuestTemplateService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public QuestTemplateService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<QuestTemplateDto> CreateQuestTemplateAsync(CreateQuestTemplateDto dto)
    {
        var questTemplate = new QuestTemplate()
        {
            Title = dto.Title,
            Description = dto.Description,
            Difficulty = dto.Difficulty
        };

        _db.QuestTemplates.Add(questTemplate);
        await _db.SaveChangesAsync();

        return _mapper.Map<QuestTemplateDto>(questTemplate);
    }

    public async Task<QuestTemplateDto> UpdateQuestTemplateAsync(string id, UpdateQuestTemplateDto dto)
    {
        var existingQuest = await _db.QuestTemplates.FindAsync(id);
        if (existingQuest == null) throw new KeyNotFoundException("Quest does not exist.");

        existingQuest.Title = dto.Title;
        existingQuest.Description = dto.Description;
        existingQuest.Difficulty = dto.Difficulty;

        await _db.SaveChangesAsync();

        return _mapper.Map<QuestTemplateDto>(existingQuest);
    }

    public async Task<QuestTemplateDto> DeleteQuestTemplateAsync(string id)
    {
        var existingQuestTemplate = await _db.QuestTemplates.FindAsync(id);
        if (existingQuestTemplate == null) throw new KeyNotFoundException("Quest template does not exist.");

        _db.Remove(existingQuestTemplate);
        await _db.SaveChangesAsync();

        return _mapper.Map<QuestTemplateDto>(existingQuestTemplate);
    }

    public async Task<List<QuestTemplateDto>> GetAllQuestTemplatesAsync(GetAllQuestQueryParams queryParams)
    {
        var query = _db.QuestTemplates.AsQueryable();

        if (queryParams.IsActive != null) query = query.Where(qt => qt.IsActive);

        var questTemplates = await query.ToListAsync();
        return _mapper.Map<List<QuestTemplateDto>>(questTemplates);
    }

    public async Task<QuestTemplateDto> GetQuestTemplateByIdAsync(string id)
    {
        var questTemplate = await _db.QuestTemplates
            .Include(q => q.Actions)
            .Include(q => q.Punishments)
            .Include(q => q.Rewards)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (questTemplate == null) throw new KeyNotFoundException("Quest not found");

        return _mapper.Map<QuestTemplateDto>(questTemplate);
    }
}