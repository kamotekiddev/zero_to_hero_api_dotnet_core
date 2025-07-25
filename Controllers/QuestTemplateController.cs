using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/quests")]
    [ApiController]
    public class QuestTemplateController : ControllerBase
    {
        private readonly IQuestTemplateService _questTemplateService;

        public QuestTemplateController(IQuestTemplateService questTemplateService)
        {
            _questTemplateService = questTemplateService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetAllQuestQueryParams queryParams)
        {
            var quests = await _questTemplateService.GetAllQuestTemplatesAsync(queryParams);
            return Ok(new { message = "Success", data = quests });
        }

        [HttpGet("{questTemplateId}")]
        public async Task<ActionResult> GetById([FromRoute] string questTemplateId)
        {
            var quest = await _questTemplateService.GetQuestTemplateByIdAsync(questTemplateId);
            return Ok(new { message = "Success", data = quest });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> CreateQuestTemplate([FromBody] CreateQuestTemplateDto dto)
        {
            var entity = await _questTemplateService.CreateQuestTemplateAsync(dto);
            return Created(nameof(CreateQuestTemplate), new { message = "success", data = entity });
        }

        [HttpPut("{questTemplateId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> UpdateQuestTemplate([FromRoute] string questTemplateId,
            [FromBody] UpdateQuestTemplateDto dto)
        {
            var entity = await _questTemplateService.UpdateQuestTemplateAsync(questTemplateId, dto);
            return Ok(new { message = "success", data = entity });
        }

        [HttpDelete("{questTemplateId}")]
        public async Task<ActionResult> DeleteQuestTemplate([FromRoute] string questTemplateId)
        {
            var entity = await _questTemplateService.DeleteQuestTemplateAsync(questTemplateId);
            return Ok(new { message = "Success", data = entity });
        }
    }
}