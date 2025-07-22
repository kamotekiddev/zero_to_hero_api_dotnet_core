using Quartz;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Enums;
using ZeroToHeroAPI.Interface;

namespace ZeroToHeroAPI.BackgroundJobs;

public class AutoFailQuestJob : IJob
{
    private readonly IDailyQuestService _dailyQuestService;
    private readonly IPlayerService _playerService;
    private readonly ApplicationDbContext _db;

    public AutoFailQuestJob(IDailyQuestService dailyQuestService, IPlayerService playerService, ApplicationDbContext db)
    {
        _dailyQuestService = dailyQuestService;
        _playerService = playerService;
        _db = db;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var dailyQuests = await _dailyQuestService.GetFailingQuest();

        foreach (var dailyQuest in dailyQuests)
        {
            var questTemplate = dailyQuest.QuestTemplate;
            var punishments = questTemplate?.Punishments;

            var isAlreadyFailed = dailyQuest.QuestStatus == nameof(DailyQuestStatusEnum.Failed);
            var currentDayQuest = dailyQuest.DateAssigned > DateTime.UtcNow;

            if (isAlreadyFailed || currentDayQuest || dailyQuest.IsCompleted) continue;

            await _dailyQuestService.QuestFailedAsync(dailyQuest.Id);

            foreach (var punishment in punishments!)
            {
                if (punishment.PunishmentTypeEnum != QuestPunishmentTypeEnum.ExpLoss) continue;

                await _playerService.UpdatePlayerAsync(dailyQuest.PlayerId,
                    ConvertIntoNegativeNumber(punishment.Value));
            }
        }
    }

    private static int ConvertIntoNegativeNumber(int number) => number * -1;
}