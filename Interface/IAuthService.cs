using ZeroToHeroAPI.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IAuthService
{
    Task<(string AccessToken, string RefreshToken)> LoginAsync(LoginDto dto);
    Task<(string AccessToken, string RefreshToken)> RegisterAsync(RegisterDto dto);
    Task<(string AccessToken, string RefreshToken)> RefreshAsync(string refreshToken);
}