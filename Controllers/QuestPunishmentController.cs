using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Route("api/quest/punishments")]
    [ApiController]
    public class QuestPunishmentController : ControllerBase
    {
        private readonly IQuestPunishmentService _questPunishmentService;

        public QuestPunishmentController(IQuestPunishmentService questPunishmentService)
        {
            _questPunishmentService = questPunishmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestPunishmentDto>>> GetAllQuestPunishments()
        {
            var questPunishments = await _questPunishmentService.GetAllQuestPunishmentsAsync();
            return Ok(new { message = "Success", data = questPunishments });
        }

        [HttpGet("{questPunishmentId}")]
        public async Task<ActionResult<QuestPunishmentDto>> GetQuestPunishmentById([FromRoute] string questPunishmentId)
        {
            var questPunishment = await _questPunishmentService.GetQuestPunishmentByIdAsync(questPunishmentId);
            return Ok(new { message = "Success", data = questPunishment });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> CreateQuestPunishment([FromBody] CreateQuestPunishmentDto dto)
        {
            var entity = await _questPunishmentService.CreateQuestPunishmentAsync(dto);
            return Created(nameof(CreateQuestPunishment), new { message = "Success", data = entity });
        }

        [HttpPut("{questPunishmentId}")]
        public async Task<IActionResult> UpdateQuestPunishment(
            [FromRoute] string questPunishmentId, [FromBody] UpdateQuestPunishmentDto dto)
        {
            var entity = await _questPunishmentService.UpdateQuestPunishmentAsync(questPunishmentId, dto);
            return Ok(new { message = "Success", data = entity });
        }

        [HttpDelete("{questPunishmentId}")]
        public async Task<ActionResult> DeleteQuestPunishment([FromRoute] string questPunishmentId)
        {
            var entity = await _questPunishmentService.DeleteQuestPunishmentAsync(questPunishmentId);
            return Ok(new { message = "Success", data = entity });
        }
    }
}