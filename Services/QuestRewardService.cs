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
            MinValue = dto.MinValue,
            MaxValue = dto.MaxValue
        };

        _context.QuestRewards.Add(entity);
        await _context.SaveChangesAsync();

        return new QuestRewardDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            RewardType = entity.RewardType,
            MinValue = entity.MinValue, MaxValue = entity.MaxValue
        };
    }

    public async Task<QuestRewardDto> UpdateQuestRewardAsync(string id, UpdateQuestRewardDto dto)
    {
        var entity = await _context.QuestRewards.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest reward does not exist.");

        entity.QuestTemplateId = dto.QuestTemplateId;
        entity.RewardType = dto.RewardType;
        entity.MinValue = dto.MinValue;
        entity.MaxValue = dto.MaxValue;

        _context.QuestRewards.Update(entity);
        await _context.SaveChangesAsync();

        return new QuestRewardDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            RewardType = entity.RewardType,
            MinValue = entity.MinValue, MaxValue = entity.MaxValue
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
            MinValue = entity.MinValue, MaxValue = entity.MaxValue
        };
    }

    public async Task<List<QuestRewardDto>> GetAllQuestRewardsAsync()
    {
        var questRewards = await _context.QuestRewards.ToListAsync();
        return questRewards.Select(questReward => new QuestRewardDto
        {
            Id = questReward.Id,
            QuestTemplateId = questReward.QuestTemplateId,
            RewardType = questReward.RewardType,
            MinValue = questReward.MinValue, MaxValue = questReward.MaxValue
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
            MinValue = entity.MinValue, MaxValue = entity.MaxValue
        };
    }
}