using AutoMapper;
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
    private readonly IMapper _mapper;

    public PlayerService(ApplicationDbContext db,
        IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IMapper mapper)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _mapper = mapper;
    }


    public async Task<DailyQuestDto> GetPlayerQuestAsync()
    {
        var user = await GetCurrentUser();

        var dailyQuest = await _db.DailyQuests
            .Include(dailyQuest => dailyQuest.QuestTemplate)
            .ThenInclude(questTemplate => questTemplate.Actions)
            .Include(dailyQuest => dailyQuest.QuestTemplate)
            .ThenInclude(questTemplate => questTemplate.Punishments)
            .Include(dailyQuest => dailyQuest.QuestTemplate)
            .ThenInclude(questTemplate => questTemplate.Rewards)
            .Include(questTemplate => questTemplate.ActionProgresses)
            .FirstOrDefaultAsync(d => d.UserId == user.Id);

        if (dailyQuest == null) throw new KeyNotFoundException("The current user has no quest assigned");

        return _mapper.Map<DailyQuestDto>(dailyQuest);
    }

    public async Task<QuestActionProgressDto> StartActionAsync(string dailyQuestId, string actionId,
        QuestActionProgressStartDto dto)
    {
        var actionProgress = await _db.QuestActionProgresses.Include(q => q.QuestAction).FirstOrDefaultAsync(q =>
            q.DailyQuestId == dailyQuestId);

        var questAction = actionProgress?.QuestAction;

        if (actionProgress == null)
        {
            actionProgress = new QuestActionProgress
            {
                DailyQuestId = dailyQuestId,
                QuestActionId = actionId,
                ProgressValue = dto.ProgressValue
            };

            _db.QuestActionProgresses.Add(actionProgress);
        }

        else
        {
            actionProgress.ProgressValue += dto.ProgressValue;

            if (actionProgress.ProgressValue >= questAction?.TargetValue)
                actionProgress.IsActionCompleted = true;
        }

        await _db.SaveChangesAsync();


        return _mapper.Map<QuestActionProgressDto>(actionProgress);
    }

    private async Task<User> GetCurrentUser()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new UnauthorizedAccessException("Unauthorized");

        return user;
    }
}