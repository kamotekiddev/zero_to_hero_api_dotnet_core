using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class PlayerStatService : IPlayerStatService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public PlayerStatService(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<PlayerStatDto> InitializePlayerStatAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new InvalidOperationException("User not found");

        var existingPlayerStat = await _context.PlayerStats.FirstOrDefaultAsync(u => u.UserId == userId);
        if (existingPlayerStat != null) throw new InvalidOperationException("Player stat already exists.");

        var newPlayerStat = new PlayerStat()
        {
            UserId = userId,
        };

        await _context.PlayerStats.AddAsync(newPlayerStat);
        await _context.SaveChangesAsync();

        return new PlayerStatDto()
        {
            Id = newPlayerStat.Id,
            CurrentLevel = newPlayerStat.CurrentLevel,
            MaxLevel = newPlayerStat.MaxLevel,
            CurrentExp = newPlayerStat.CurrentExp,
            NextLevelExp = newPlayerStat.NextLevelExp,
            UserId = newPlayerStat.UserId
        };
    }

    public async Task<(PlayerStatDto playerStat, List<PlayerAction> actions)> UpdatePlayerStatAsync(string playerStatId,
        UpdatePlayerStatsDto updatePlayerStatsDto)
    {
        var actionsPerformed = new List<PlayerAction>();
        var playerStat = await _context.PlayerStats.FindAsync(playerStatId);
        if (playerStat == null) throw new InvalidOperationException("Player not found");

        playerStat.CurrentExp += updatePlayerStatsDto.ExpGained;

        if (updatePlayerStatsDto.ExpGained > 0)
            actionsPerformed.Add(PlayerAction.ExpGained);
        else if (updatePlayerStatsDto.ExpGained < 0)
            actionsPerformed.Add(PlayerAction.ExpDecreased);

        // Level Up
        while (playerStat.CurrentExp >= GetNextLevelExp(playerStat.CurrentLevel))
        {
            playerStat.CurrentExp -= GetNextLevelExp(playerStat.CurrentLevel);
            playerStat.CurrentLevel++;
            actionsPerformed.Add(PlayerAction.LeveledUp);
        }

        // Level Down
        while (playerStat.CurrentExp < 0 && playerStat.CurrentLevel > 1)
        {
            playerStat.CurrentLevel--;
            playerStat.CurrentExp += GetNextLevelExp(playerStat.CurrentLevel);
            actionsPerformed.Add(PlayerAction.LeveledDown);
        }

        playerStat.NextLevelExp = GetNextLevelExp(playerStat.CurrentLevel);

        await _context.SaveChangesAsync();

        var responseDto = new PlayerStatDto
        {
            Id = playerStat.Id,
            UserId = playerStat.UserId,
            CurrentExp = playerStat.CurrentExp,
            CurrentLevel = playerStat.CurrentLevel,
            NextLevelExp = playerStat.NextLevelExp,
        };

        return (responseDto, actionsPerformed);
    }

    int GetNextLevelExp(int level) => 50 * level * level;
}