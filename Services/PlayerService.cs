using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Enums;
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
        var actionProgress = await _db.QuestActionProgresses
            .Include(q => q.QuestAction)
            .FirstOrDefaultAsync(q =>
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
        if (_httpContextAccessor.HttpContext?.User == null) throw new UnauthorizedAccessException();

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null) throw new UnauthorizedAccessException();

        return user;
    }

    public async Task<PlayerDto> InitializePlayerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new InvalidOperationException("User not found");

        var existingEntity = await _db.Player.FirstOrDefaultAsync(u => u.UserId == userId);
        if (existingEntity != null) throw new InvalidOperationException("Player already exists.");

        var entity = new Player()
        {
            UserId = userId,
        };

        _db.Player.Add(entity);
        await _db.SaveChangesAsync();

        return _mapper.Map<PlayerDto>(entity);
    }

    public async Task<(PlayerDto player, List<PlayerActionEnum> actions)> UpdatePlayerAsync(
        string playerId,
        UpdatePlayerDto dto)
    {
        var actionsPerformed = new List<PlayerActionEnum>();
        var playerHistory = new List<PlayerHistory>();

        var player = await _db.Player.FindAsync(playerId);
        if (player == null) throw new InvalidOperationException("Player not found");

        await using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            ApplyExpChange(player, dto.ExpGained, actionsPerformed, playerHistory);
            ApplyLevelChanges(player, actionsPerformed, playerHistory);

            player.NextLevelExp = GetNextLevelExp(player.CurrentLevel);

            _db.PlayerHistory.AddRange(playerHistory);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return (_mapper.Map<PlayerDto>(player), actionsPerformed);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private static void ApplyExpChange(Player player, int expChange, List<PlayerActionEnum> actions,
        List<PlayerHistory> history)
    {
        if (expChange == 0) return;

        var originalExp = player.CurrentExp;
        player.CurrentExp += expChange;

        var action = expChange > 0 ? PlayerActionEnum.ExpGained : PlayerActionEnum.ExpDecreased;
        actions.Add(action);

        history.Add(new PlayerHistory
        {
            PlayerId = player.Id,
            OldValue = originalExp,
            NewValue = player.CurrentExp,
            Action = action,
            CreatedAt = DateTime.UtcNow
        });
    }

    private static void ApplyLevelChanges(Player player, List<PlayerActionEnum> actions,
        List<PlayerHistory> history)
    {
        // Level Up
        while (player.CurrentExp >= player.NextLevelExp)
        {
            var oldLevel = player.CurrentLevel;
            player.CurrentExp -= GetNextLevelExp(oldLevel);
            player.CurrentLevel++;

            actions.Add(PlayerActionEnum.LeveledUp);
            history.Add(CreateLevelHistory(player, oldLevel, player.CurrentLevel, PlayerActionEnum.LeveledUp));
        }

        // Level Down
        while (player.CurrentExp < 0 && player.CurrentLevel > 1)
        {
            var oldLevel = player.CurrentLevel;
            player.CurrentLevel--;
            player.CurrentExp += GetNextLevelExp(player.CurrentLevel);

            actions.Add(PlayerActionEnum.LeveledDown);
            history.Add(CreateLevelHistory(player, oldLevel, player.CurrentLevel, PlayerActionEnum.LeveledDown));
        }
    }

    private static PlayerHistory CreateLevelHistory(Player player, int oldLevel, int newLevel, PlayerActionEnum action)
    {
        return new PlayerHistory
        {
            PlayerId = player.Id,
            OldValue = oldLevel,
            NewValue = newLevel,
            Action = action,
            CreatedAt = DateTime.UtcNow
        };
    }

    private static int GetNextLevelExp(int level) => 50 * level * level;
}