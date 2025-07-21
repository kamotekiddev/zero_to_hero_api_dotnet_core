using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class DailyQuestService : IDailyQuestService
{
    private readonly ApplicationDbContext _db;

    public DailyQuestService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DailyQuestDto>> GetAllDailyQuestAsync()
    {
        var dailyQuests = await _db.DailyQuests.ToListAsync();
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
        var entity = await _db.DailyQuests.FindAsync(id);
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

        _db.Add(entity);
        await _db.SaveChangesAsync();

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
        var entity = await _db.DailyQuests.FindAsync(dailyQuestId);
        if (entity == null) throw new KeyNotFoundException("Daily quest does not exist");

        entity.QuestTemplateId = dto.QuestTemplateId;
        await _db.SaveChangesAsync();

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
        var entity = await _db.DailyQuests.FindAsync(dailyQuestId);
        if (entity == null) throw new KeyNotFoundException("Daily quest does not exist");

        entity.UserId = dto.UserId;
        entity.DateAssigned = DateTime.UtcNow;

        await _db.SaveChangesAsync();

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

    public async Task<bool> CreateAndAssignDailyQuestToUsers(List<DailyQuest> dailyQuests)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            _db.DailyQuests.AddRange(dailyQuests);
            var result = await _db.SaveChangesAsync();

            if (result != dailyQuests.Count) return false;

            await transaction.CommitAsync();

            return true;
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message);
        }
    }
}