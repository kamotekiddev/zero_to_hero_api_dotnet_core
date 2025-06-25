using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models.Dtos;

namespace ZeroToHeroAPI.Controllers
{
    [Route("api/admin/quests")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var quests = await _questService.GetAllQuestTemplatesAsync();
            return Ok(new { message = "Success", data = quests });
        }

        [HttpGet("{questTemplateId}")]
        public async Task<ActionResult> GetById([FromRoute] string questTemplateId)
        {
            var quest = await _questService.GetQuestTemplateByIdAsync(questTemplateId);
            return Ok(new { message = "Success", data = quest });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> CreateQuestTemplate([FromBody] CreateQuestTemplateDto dto)
        {
            var entity = await _questService.CreateQuestTemplateAsync(dto);
            return Created(nameof(CreateQuestTemplate), new { message = "success", data = entity });
        }

        [HttpPut("{questTemplateId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> UpdateQuestTemplate([FromRoute] string questTemplateId,
            [FromBody] UpdateQuestTemplateDto dto)
        {
            var entity = await _questService.UpdateQuestTemplateAsync(questTemplateId, dto);
            return Ok(new { message = "success", data = entity });
        }

        [HttpDelete("{questTemplateId}")]
        public async Task<ActionResult> DeleteQuestTemplate([FromRoute] string questTemplateId)
        {
            var entity = await _questService.DeleteQuestTemplateAsync(questTemplateId);
            return Ok(new { message = "Success", data = entity });
        }
    }
}