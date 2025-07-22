using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/quest/daily")]
    [ApiController]
    public class DailyQuestController : ControllerBase
    {
        private readonly IDailyQuestService _dailyQuestService;

        public DailyQuestController(IDailyQuestService dailyQuestService)
        {
            _dailyQuestService = dailyQuestService;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllDailyQuest([FromQuery] GetAllDailyQuestQueryParams queryParams)
        {
            var dailyQuests = await _dailyQuestService.GetAllDailyQuestAsync(queryParams);
            return Ok(new { message = "Success", data = dailyQuests });
        }

        [HttpGet("{dailyQuestId}")]
        public async Task<ActionResult> GetDailyQuestById([FromRoute] string dailyQuestId)
        {
            var dailyQuest = await _dailyQuestService.GetDailyQuestByIdAsync(dailyQuestId);
            return Ok(new { message = "Success", data = dailyQuest });
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> CreateDailyQuest([FromBody] CreateDailyQuestDto dto)
        {
            var entity = await _dailyQuestService.CreateDailyQuest(dto);
            return Created(nameof(CreateDailyQuest), new { message = "Success", data = entity });
        }

        [HttpPut("{dailyQuestId}")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> UpdateDailyQuest([FromRoute] string dailyQuestId,
            [FromBody] UpdateDailyQuestDto dto)
        {
            var entity = await _dailyQuestService.UpdateDailyQuestAsync(dailyQuestId, dto);
            return Ok(new { message = "Success", data = entity });
        }


        [HttpPut("{dailyQuestId}/assign")]
        [ServiceFilter(typeof(ValidateDtoFilter))]
        public async Task<ActionResult> AssignDailyQuestToUser([FromRoute] string dailyQuestId,
            [FromBody] AssignDailyQuestDto dto)
        {
            var entity = await _dailyQuestService.AssignQuestToUser(dailyQuestId, dto);
            return Ok(new { message = "Success", data = entity });
        }
    }
}