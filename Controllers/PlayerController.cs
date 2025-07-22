using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Authorize]
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("quest/daily")]
        public async Task<IActionResult> GetUserDailyQuest()
        {
            var entity = await _playerService.GetPlayerQuestAsync();
            return Ok(new { message = "Success", data = entity });
        }

        [HttpPut("quest/daily/{dailyQuestId}/action/{actionId}/start")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> StartQuestAction([FromRoute] string dailyQuestId, string actionId,
            [FromBody] QuestActionProgressStartDto dto)
        {
            var entity = await _playerService.StartActionAsync(dailyQuestId, actionId, dto);
            return Ok(new { message = "Success", data = entity });
        }

        [HttpPost("{userId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> InitializePlayer([FromRoute] string userId)
        {
            var res = await _playerService.InitializePlayerAsync(userId);
            return Created(nameof(InitializePlayer), res);
        }

        [HttpPost("update/{playerId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> UpdatePlayer([FromRoute] string playerId,
            [FromBody] UpdatePlayerDto dto)
        {
            var (player, actions) = await _playerService.UpdatePlayerAsync(playerId, dto.ExpGained);
            return Ok(new { Data = player, Actions = actions });
        }
    }
}