using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models.Quest;

namespace ZeroToHeroAPI.Services;

public class QuestPunishmentService : IQuestPunishmentService
{
    private readonly ApplicationDbContext _context;

    public QuestPunishmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuestPunishmentDto> CreateQuestPunishmentAsync(CreateQuestPunishmentDto dto)
    {
        var entity = new QuestPunishment
        {
            QuestTemplateId = dto.QuestTemplateId,
            PunishmentType = dto.PunishmentType,
            MinValue = dto.MinValue,
            MaxValue = dto.MaxValue
        };

        _context.QuestPunishments.Add(entity);
        await _context.SaveChangesAsync();

        return new QuestPunishmentDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            PunishmentType = entity.PunishmentType,
            MinValue = entity.MinValue,
            MaxValue = entity.MaxValue
        };
    }


    public async Task<QuestPunishmentDto> UpdateQuestPunishmentAsync(string id, UpdateQuestPunishmentDto dto)
    {
        var entity = await _context.QuestPunishments.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest punishment does not exist.");

        entity.QuestTemplateId = dto.QuestTemplateId;
        entity.PunishmentType = dto.PunishmentType;
        entity.MinValue = dto.MinValue;
        entity.MaxValue = dto.MaxValue;

        _context.QuestPunishments.Update(entity);
        await _context.SaveChangesAsync();

        return new QuestPunishmentDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            PunishmentType = entity.PunishmentType,
            MinValue = entity.MinValue,
            MaxValue = entity.MaxValue
        };
    }

    public async Task<QuestPunishmentDto> DeleteQuestPunishmentAsync(string id)
    {
        var entity = await _context.QuestPunishments.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest punishment does not exist.");

        _context.QuestPunishments.Remove(entity);
        await _context.SaveChangesAsync();

        return new QuestPunishmentDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            PunishmentType = entity.PunishmentType,
            MinValue = entity.MinValue,
            MaxValue = entity.MaxValue
        };
    }

    public async Task<List<QuestPunishmentDto>> GetAllQuestPunishmentsAsync()
    {
        var questPunishments = await _context.QuestPunishments.ToListAsync();

        return questPunishments.Select(questPunishment => new QuestPunishmentDto
        {
            Id = questPunishment.Id,
            QuestTemplateId = questPunishment.QuestTemplateId,
            PunishmentType = questPunishment.PunishmentType,
            MinValue = questPunishment.MinValue,
            MaxValue = questPunishment.MaxValue
        }).ToList();
    }

    public async Task<QuestPunishmentDto> GetQuestPunishmentByIdAsync(string id)
    {
        var entity = await _context.QuestPunishments.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest punishment does not exist.");

        return new QuestPunishmentDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            PunishmentType = entity.PunishmentType,
            MinValue = entity.MinValue,
            MaxValue = entity.MaxValue
        };
    }
}