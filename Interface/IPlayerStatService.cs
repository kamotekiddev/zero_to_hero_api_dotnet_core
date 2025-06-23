using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Data.Dtos;

namespace ZeroToHeroAPI.Interface;

public interface IPlayerStatService
{
    Task<InitializePlayerStatDto> InitializePlayerStatAsync(
        string userId);
}