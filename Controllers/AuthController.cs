using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var (accessToken, refreshToken) = await _authService.RegisterAsync(dto);
        return Ok(new { Message = "Success", AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var (accessToken, refreshToken) = await _authService.LoginAsync(dto);
        return Ok(new { Message = "Success", AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
    {
        var (accessToken, refreshToken) = await _authService.RefreshAsync(dto.RefreshToken);
        return Ok(new { Message = "Success", AccessToken = accessToken, RefreshToken = refreshToken });
    }
}