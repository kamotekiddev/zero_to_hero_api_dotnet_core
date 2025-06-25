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

        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> CreateQuestTemplate([FromBody] CreateQuestTemplateDto questTemplateDto)
        {
            var questTemplates = await _questService.CreateQuestTemplateAsync(questTemplateDto);
            return Created(nameof(CreateQuestTemplate), new { message = "success", data = questTemplates });
        }
    }
}