using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Route("api/player-stats")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerStatService _playerStatService;

        public PlayerController(IPlayerStatService playerStatService)
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
    }
}