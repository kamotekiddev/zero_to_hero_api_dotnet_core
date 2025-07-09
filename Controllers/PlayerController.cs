using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("daily-quest")]
        public async Task<IActionResult> GetUserDailyQuest()
        {
            var playerQuest = await _playerService.GetPlayerQuestAsync();
            return Ok(new { message = "Success", data = playerQuest });
        }
    }
}