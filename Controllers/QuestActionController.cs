using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Route("api/quest/actions")]
    [ApiController]
    public class QuestActionController : ControllerBase
    {
        private readonly IQuestActionService _questActionService;

        public QuestActionController(IQuestActionService questActionService)
        {
            _questActionService = questActionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var questActions = await _questActionService.GetAllQuestActionsAsync();
            return Ok(new { message = "Success", data = questActions });
        }

        [HttpGet("{questActionId}")]
        public async Task<ActionResult> GetById([FromRoute] string questActionId)
        {
            var questAction = await _questActionService.GetQuestActionByIdAsync(questActionId);
            return Ok(new { message = "Success", data = questAction });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> CreateQuestAction([FromBody] CreateQuestActionDto dto)
        {
            var entity = await _questActionService.CreateQuestActionAsync(dto);
            return Created(nameof(CreateQuestAction), new { message = "success", data = entity });
        }

        [HttpPut("{questActionId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> UpdateQuestAction([FromRoute] string questActionId,
            [FromBody] UpdateQuestActionDto dto)
        {
            var entity = await _questActionService.UpdateQuestActionAsync(questActionId, dto);
            return Ok(new { message = "success", data = entity });
        }

        [HttpDelete("{questActionId}")]
        public async Task<ActionResult> DeleteQuestAction([FromRoute] string questActionId)
        {
            var entity = await _questActionService.DeleteQuestActionAsync(questActionId);
            return Ok(new { message = "Success", data = entity });
        }
    }
}