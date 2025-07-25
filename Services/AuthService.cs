using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly TokenService _tokenService;
    private readonly IPlayerService _playerService;

    public AuthService(UserManager<User> userManager, ApplicationDbContext db, TokenService tokenService,
        RoleManager<IdentityRole> roleManager, IPlayerService playerService)
    {
        _db = db;
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _playerService = playerService;
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) throw new BadHttpRequestException("Invalid Credentials");

        var passwordMatched = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!passwordMatched) throw new BadHttpRequestException("Invalid Credentials");

        var accessToken = await _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();

        return (accessToken, refreshToken.Token);
    }

    public async Task<(string AccessToken, string RefreshToken)> RegisterAsync(RegisterDto dto)
    {
        const string role = "User";

        var userExists = await _userManager.FindByEmailAsync(dto.Email);
        if (userExists != null)
            throw new BadHttpRequestException("Email already registered");

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new BadHttpRequestException(
                    $"Register Failed {string.Join("; ", result.Errors.Select(e => e.Description))}");

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
            await _playerService.InitializePlayerAsync(user.Id);

            var accessToken = await _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return (accessToken, refreshToken.Token);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshAsync(string token)
    {
        var existingRefreshToken = await _db.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (existingRefreshToken == null || existingRefreshToken.Expiration < DateTime.UtcNow)
            throw new BadHttpRequestException("Refresh token is expired");

        var accessToken = await _tokenService.GenerateAccessToken(existingRefreshToken.User);
        var refreshToken = _tokenService.GenerateRefreshToken(existingRefreshToken.UserId);

        existingRefreshToken.Token = refreshToken.Token;
        existingRefreshToken.Expiration = refreshToken.Expiration;

        await _db.SaveChangesAsync();

        return (accessToken, refreshToken.Token);
    }
}