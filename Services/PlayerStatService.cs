using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Data.Dtos;
using ZeroToHeroAPI.Interface;

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

    public async Task<InitializePlayerStatDto> InitializePlayerStatAsync(string userId)
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

        return new InitializePlayerStatDto()
        {
            Id = newPlayerStat.Id,
            CurrentLevel = newPlayerStat.CurrentLevel,
            MaxLevel = newPlayerStat.MaxLevel,
            CurrentExp = newPlayerStat.CurrentExp,
            NextLevelExp = newPlayerStat.NextLevelExp,
            UserId = newPlayerStat.UserId
        };
    }
}