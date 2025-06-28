using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class QuestRewardService : IQuestRewardService
{
    private readonly ApplicationDbContext _context;

    public QuestRewardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuestRewardDto> CreateQuestRewardAsync(CreateQuestRewardDto dto)
    {
        var entity = new QuestReward
        {
            QuestTemplateId = dto.QuestTemplateId,
            RewardType = dto.RewardType,
            Value = dto.Value,
        };

        _context.QuestRewards.Add(entity);
        await _context.SaveChangesAsync();

        return new QuestRewardDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            RewardType = entity.RewardType,
            Value = dto.Value,
        };
    }

    public async Task<QuestRewardDto> UpdateQuestRewardAsync(string id, UpdateQuestRewardDto dto)
    {
        var entity = await _context.QuestRewards.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest reward does not exist.");

        entity.QuestTemplateId = dto.QuestTemplateId;
        entity.RewardType = dto.RewardType;
        entity.Value = dto.Value;

        _context.QuestRewards.Update(entity);
        await _context.SaveChangesAsync();

        return new QuestRewardDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            RewardType = entity.RewardType,
            Value = entity.Value,
        };
    }

    public async Task<QuestRewardDto> DeleteQuestRewardAsync(string id)
    {
        var entity = await _context.QuestRewards.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest reward does not exist.");

        _context.QuestRewards.Remove(entity);
        await _context.SaveChangesAsync();

        return new QuestRewardDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            RewardType = entity.RewardType,
            Value = entity.Value,
        };
    }

    public async Task<List<QuestRewardDto>> GetAllQuestRewardsAsync()
    {
        var questRewards = await _context.QuestRewards.ToListAsync();
        return questRewards.Select(q => new QuestRewardDto
        {
            Id = q.Id,
            QuestTemplateId = q.QuestTemplateId,
            RewardType = q.RewardType,
            Value = q.Value,
        }).ToList();
    }

    public async Task<QuestRewardDto> GetQuestRewardByIdAsync(string id)
    {
        var entity = await _context.QuestRewards.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest reward does not exist.");

        return new QuestRewardDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            RewardType = entity.RewardType,
            Value = entity.Value
        };
    }
}