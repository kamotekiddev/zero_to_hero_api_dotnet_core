using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

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
            Value = dto.Value,
        };

        _context.QuestPunishments.Add(entity);
        await _context.SaveChangesAsync();

        return new QuestPunishmentDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            PunishmentTypeEnum = entity.PunishmentType,
            Value = dto.Value,
        };
    }


    public async Task<QuestPunishmentDto> UpdateQuestPunishmentAsync(string id, UpdateQuestPunishmentDto dto)
    {
        var entity = await _context.QuestPunishments.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest punishment does not exist.");

        entity.QuestTemplateId = dto.QuestTemplateId;
        entity.PunishmentType = dto.PunishmentType;
        entity.Value = dto.Value;

        _context.QuestPunishments.Update(entity);
        await _context.SaveChangesAsync();

        return new QuestPunishmentDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            PunishmentTypeEnum = entity.PunishmentType,
            Value = entity.Value,
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
            PunishmentTypeEnum = entity.PunishmentType,
            Value = entity.Value,
        };
    }

    public async Task<List<QuestPunishmentDto>> GetAllQuestPunishmentsAsync()
    {
        var questPunishments = await _context.QuestPunishments.ToListAsync();

        return questPunishments.Select(q => new QuestPunishmentDto
        {
            Id = q.Id,
            QuestTemplateId = q.QuestTemplateId,
            PunishmentTypeEnum = q.PunishmentType,
            Value = q.Value,
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
            PunishmentTypeEnum = entity.PunishmentType,
            Value = entity.Value,
        };
    }
}