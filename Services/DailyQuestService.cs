using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class DailyQuestService : IDailyQuestService
{
    private readonly ApplicationDbContext _context;

    public DailyQuestService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DailyQuestDto>> GetAllDailyQuestAsync()
    {
        var dailyQuests = await _context.DailyQuests.ToListAsync();
        return dailyQuests.Select(q => new DailyQuestDto
        {
            Id = q.Id,
            UserId = q.UserId,
            QuestTemplateId = q.QuestTemplateId,
            DateAssigned = q.DateAssigned,
            DateCompleted = q.DateCompleted,
            IsCompleted = q.IsCompleted,
            QuestStatus = q.QuestStatus,
        });
    }

    public async Task<DailyQuestDto> GetDailyQuestByIdAsync(string id)
    {
        var entity = await _context.DailyQuests.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Daily quest does not exist");

        return new DailyQuestDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            QuestTemplateId = entity.QuestTemplateId,
            DateAssigned = entity.DateAssigned,
            DateCompleted = entity.DateCompleted,
            IsCompleted = entity.IsCompleted,
            QuestStatus = entity.QuestStatus,
        };
    }

    public async Task<DailyQuestDto> CreateDailyQuest(CreateDailyQuestDto dto)
    {
        var entity = new DailyQuest
        {
            QuestTemplateId = dto.QuestTemplateId,
        };

        _context.Add(entity);
        await _context.SaveChangesAsync();

        return new DailyQuestDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            QuestTemplateId = entity.QuestTemplateId,
            DateAssigned = entity.DateAssigned,
            DateCompleted = entity.DateCompleted,
            IsCompleted = entity.IsCompleted,
            QuestStatus = entity.QuestStatus,
        };
    }

    public async Task<DailyQuestDto> UpdateDailyQuestAsync(string dailyQuestId, UpdateDailyQuestDto dto)
    {
        var entity = await _context.DailyQuests.FindAsync(dailyQuestId);
        if (entity == null) throw new KeyNotFoundException("Daily quest does not exist");

        entity.QuestTemplateId = dto.QuestTemplateId;
        await _context.SaveChangesAsync();

        return new DailyQuestDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            QuestTemplateId = entity.QuestTemplateId,
            DateAssigned = entity.DateAssigned,
            DateCompleted = entity.DateCompleted,
            IsCompleted = entity.IsCompleted,
        };
    }

    public async Task<DailyQuestDto> AssignQuestToUser(string dailyQuestId, AssignDailyQuestDto dto)
    {
        var entity = await _context.DailyQuests.FindAsync(dailyQuestId);
        if (entity == null) throw new KeyNotFoundException("Daily quest does not exist");

        entity.UserId = dto.UserId;
        entity.DateAssigned = DateTime.UtcNow;

        _context.Update(entity);
        await _context.SaveChangesAsync();

        return new DailyQuestDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            QuestTemplateId = entity.QuestTemplateId,
            DateAssigned = entity.DateAssigned,
            DateCompleted = entity.DateCompleted,
            IsCompleted = entity.IsCompleted,
            QuestStatus = entity.QuestStatus,
        };
    }
}