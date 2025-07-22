using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Enums;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class DailyQuestService : IDailyQuestService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public DailyQuestService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DailyQuestDto>> GetAllDailyQuestAsync(GetAllDailyQuestQueryParams queryParams)
    {
        var query = _db.DailyQuests.AsQueryable();

        if (queryParams.IsCompleted != null)
            query = query.Where(dq => !dq.IsCompleted && dq.DateAssigned < DateTime.UtcNow);


        var dailyQuests = await query.ToListAsync();
        return _mapper.Map<IEnumerable<DailyQuestDto>>(dailyQuests);
    }

    public async Task<List<DailyQuestDto>> GetFailingQuest()
    {
        var dailyQuests = await _db.DailyQuests
            .Include(dq => dq.QuestTemplate)
            .ThenInclude(qt => qt.Punishments)
            .Where(dq => !dq.IsCompleted && dq.DateAssigned < DateTime.UtcNow)
            .ToListAsync();

        return _mapper.Map<List<DailyQuestDto>>(dailyQuests);
    }

    public async Task<DailyQuestDto> QuestFailedAsync(string dailyQuestId)
    {
        var dailyQuest = await FindDailyQuest(dailyQuestId);

        dailyQuest.QuestStatus = nameof(DailyQuestStatusEnum.Failed);
        await _db.SaveChangesAsync();

        return _mapper.Map<DailyQuestDto>(dailyQuest);
    }

    public async Task<DailyQuestDto> GetDailyQuestByIdAsync(string id)
    {
        var entity = await FindDailyQuest(id);
        return _mapper.Map<DailyQuestDto>(entity);
    }

    public async Task<DailyQuestDto> CreateDailyQuest(CreateDailyQuestDto dto)
    {
        var entity = new DailyQuest
        {
            QuestTemplateId = dto.QuestTemplateId,
        };

        _db.Add(entity);
        await _db.SaveChangesAsync();

        return _mapper.Map<DailyQuestDto>(entity);
    }

    public async Task<DailyQuestDto> UpdateDailyQuestAsync(string dailyQuestId, UpdateDailyQuestDto dto)
    {
        var entity = await FindDailyQuest(dailyQuestId);

        entity.QuestTemplateId = dto.QuestTemplateId;
        await _db.SaveChangesAsync();

        return _mapper.Map<DailyQuestDto>(entity);
    }

    public async Task<DailyQuestDto> AssignQuestToUser(string dailyQuestId, AssignDailyQuestDto dto)
    {
        var entity = await FindDailyQuest(dailyQuestId);

        entity.PlayerId = dto.PlayerId;
        entity.DateAssigned = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return _mapper.Map<DailyQuestDto>(entity);
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

    private async Task<DailyQuest> FindDailyQuest(string dailyQuestId)
    {
        var dailyQuest = await _db.DailyQuests.FindAsync(dailyQuestId);
        if (dailyQuest is null) throw new KeyNotFoundException($"{dailyQuest} Daily Quest is not found.");

        return dailyQuest;
    }
}