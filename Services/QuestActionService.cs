using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class QuestActionService : IQuestActionService
{
    private readonly ApplicationDbContext _context;

    public QuestActionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuestActionDto> CreateQuestActionAsync(CreateQuestActionDto dto)
    {
        var entity = new QuestAction
        {
            QuestTemplateId = dto.QuestTemplateId,
            ActionType = dto.ActionType,
            TargetValue = dto.TargetValue,
            Unit = dto.Unit
        };

        _context.QuestActions.Add(entity);
        await _context.SaveChangesAsync();

        return new QuestActionDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            ActionType = entity.ActionType,
            TargetValue = entity.TargetValue,
            Unit = entity.Unit
        };
    }

    public async Task<QuestActionDto> UpdateQuestActionAsync(string id, UpdateQuestActionDto dto)
    {
        var entity = await _context.QuestActions.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest action does not exist.");

        entity.ActionType = dto.ActionType;
        entity.TargetValue = dto.TargetValue;
        entity.Unit = dto.Unit;

        _context.QuestActions.Update(entity);
        await _context.SaveChangesAsync();

        return new QuestActionDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            ActionType = entity.ActionType,
            TargetValue = entity.TargetValue,
            Unit = entity.Unit
        };
    }

    public async Task<QuestActionDto> DeleteQuestActionAsync(string id)
    {
        var entity = await _context.QuestActions.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Quest action does not exist.");

        _context.QuestActions.Remove(entity);
        await _context.SaveChangesAsync();

        return new QuestActionDto
        {
            Id = entity.Id,
            QuestTemplateId = entity.QuestTemplateId,
            ActionType = entity.ActionType,
            TargetValue = entity.TargetValue,
            Unit = entity.Unit
        };
    }

    public async Task<List<QuestActionDto>> GetAllQuestActionsAsync()
    {
        var questActions = await _context.QuestActions.ToListAsync();
        return questActions.Select(questAction => new QuestActionDto
        {
            Id = questAction.Id,
            QuestTemplateId = questAction.QuestTemplateId,
            ActionType = questAction.ActionType,
            TargetValue = questAction.TargetValue,
            Unit = questAction.Unit
        }).ToList();
    }

    public async Task<QuestActionDto> GetQuestActionByIdAsync(string id)
    {
        var questAction = await _context.QuestActions.FindAsync(id);
        if (questAction == null) throw new KeyNotFoundException("Quest action not found");

        return new QuestActionDto
        {
            Id = questAction.Id,
            QuestTemplateId = questAction.QuestTemplateId,
            ActionType = questAction.ActionType,
            TargetValue = questAction.TargetValue,
            Unit = questAction.Unit
        };
    }
}