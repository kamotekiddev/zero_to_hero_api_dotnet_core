using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Route("api/player-stats")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerStatService _playerStatService;

        public PlayerStatsController(IPlayerStatService playerStatService)
        {
            _playerStatService = playerStatService;
        }

        [HttpGet("{userId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> InitializePlayerStat([FromRoute] string userId)
        {
            var res = await _playerStatService.InitializePlayerStatAsync(userId);
            return Created($"api/player-stats/{userId}", res);
        }

        [HttpPost("{playerStatId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> UpdatePlayerStatAsync([FromRoute] string playerStatId,
            [FromBody] UpdatePlayerStatsDto updatePlayerStatDto)
        {
            var (playerStat, actions) = await _playerStatService.UpdatePlayerStatAsync(
                playerStatId,
                new UpdatePlayerStatsDto() { ExpGained = updatePlayerStatDto.ExpGained });

            var formattedActions = actions.Select(action => action.ToString()).ToList();

            return Ok(new { Data = playerStat, Actions = formattedActions });
        }
    }
}