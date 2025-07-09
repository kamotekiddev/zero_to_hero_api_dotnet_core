using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class PlayerService : IPlayerService
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public PlayerService(ApplicationDbContext db,
        IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<DailyQuestDto> GetPlayerQuestAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) throw new UnauthorizedAccessException("Unauthorized");

        var dailyQuest = await _db.DailyQuests.Include(dailyQuest => dailyQuest.QuestTemplate)
            .ThenInclude(questTemplate => questTemplate.Actions).Include(dailyQuest => dailyQuest.QuestTemplate)
            .ThenInclude(questTemplate => questTemplate.Punishments).Include(dailyQuest => dailyQuest.QuestTemplate)
            .ThenInclude(questTemplate => questTemplate.Rewards)
            .FirstOrDefaultAsync(d => d.UserId == user.Id);

        if (dailyQuest == null) throw new KeyNotFoundException("The current user has no quest assigned");

        var questTemplate = dailyQuest.QuestTemplate;

        var questTemplateActions =
            questTemplate.Actions.Select(qa => new QuestActionDto
            {
                Id = qa.Id,
                ActionType = qa.ActionType,
                QuestTemplateId = qa.QuestTemplateId,
                TargetValue = qa.TargetValue,
                Unit = qa.Unit
            });

        var questPunishments = questTemplate.Punishments.Select(qp => new QuestPunishmentDto
        {
            Id = qp.Id,
            QuestTemplateId = qp.QuestTemplateId,
            PunishmentTypeEnum = qp.PunishmentType,
            Value = qp.Value
        });

        var questRewards = questTemplate.Rewards.Select(qr => new QuestRewardDto
        {
            Id = qr.Id,
            QuestTemplateId = qr.QuestTemplateId,
            Value = qr.Value
        });


        return new DailyQuestDto
        {
            Id = dailyQuest.Id,
            UserId = dailyQuest.UserId,
            DateAssigned = dailyQuest.DateAssigned,
            DateCompleted = dailyQuest.DateCompleted,
            QuestStatus = dailyQuest.QuestStatus,
            QuestTemplateId = dailyQuest.QuestTemplateId,
            QuestTemplate = new QuestTemplateDto
            {
                Id = questTemplate.Id,
                Description = questTemplate.Description,
                Difficulty = questTemplate.Difficulty,
                Title = questTemplate.Title,
                Actions = questTemplateActions,
                Punishments = questPunishments,
                Rewards = questRewards,
            },
            IsCompleted = dailyQuest.IsCompleted
        };
    }
}