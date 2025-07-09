using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/quest/rewards")]
    [ApiController]
    public class QuestRewardsController : ControllerBase
    {
        private readonly IQuestRewardService _questRewardService;

        public QuestRewardsController(IQuestRewardService questRewardService)
        {
            _questRewardService = questRewardService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var questRewards = await _questRewardService.GetAllQuestRewardsAsync();
            return Ok(new { message = "Success", data = questRewards });
        }

        [HttpGet("{questRewardId}")]
        public async Task<ActionResult> GetById([FromRoute] string questRewardId)
        {
            var questReward = await _questRewardService.GetQuestRewardByIdAsync(questRewardId);
            return Ok(new { message = "Success", data = questReward });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<IActionResult> CreateQuestReward([FromBody] CreateQuestRewardDto dto)
        {
            var entity = await _questRewardService.CreateQuestRewardAsync(dto);
            return Created(nameof(CreateQuestReward), new { message = "success", data = entity });
        }

        [HttpPut("{questRewardId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> UpdateQuestReward([FromRoute] string questRewardId,
            [FromBody] UpdateQuestRewardDto dto)
        {
            var entity = await _questRewardService.UpdateQuestRewardAsync(questRewardId, dto);
            return Ok(new { message = "Success", data = entity });
        }

        [HttpDelete("{questRewardId}")]
        public async Task<ActionResult> DeleteQuestReward([FromRoute] string questRewardId)
        {
            var entity = await _questRewardService.DeleteQuestRewardAsync(questRewardId);
            return Ok(new { message = "Success", data = entity });
        }
    }
}