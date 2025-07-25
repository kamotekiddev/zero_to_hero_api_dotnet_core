using System.ComponentModel;
using Quartz;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Enums;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.BackgroundJobs;

public class AutoAssignQuestJob : IJob
{
    private readonly IPlayerService _playerService;
    private readonly IDailyQuestService _dailyQuestService;
    private readonly IQuestTemplateService _questTemplateTemplateService;


    public AutoAssignQuestJob(IPlayerService playerService, IDailyQuestService dailyQuestService,
        IQuestTemplateService questTemplateTemplateService)
    {
        _playerService = playerService;
        _dailyQuestService = dailyQuestService;
        _questTemplateTemplateService = questTemplateTemplateService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var players = await _playerService.GetAllPlayersAsync();
        var questTemplates =
            await _questTemplateTemplateService.GetAllQuestTemplatesAsync(new GetAllQuestQueryParams()
                { IsActive = true });

        var questTemplateEasy = GetQuestTemplateBasedOnDifficulty(questTemplates, QuestDifficultyEnum.Easy);
        var questTemplateMedium = GetQuestTemplateBasedOnDifficulty(questTemplates, QuestDifficultyEnum.Medium);
        var questTemplateHard = GetQuestTemplateBasedOnDifficulty(questTemplates, QuestDifficultyEnum.Hard);

        var playerDailyQuests = new List<DailyQuest>();


        foreach (var player in players)
        {
            var dailyQuest = new DailyQuest();

            dailyQuest.QuestTemplateId = player.CurrentLevel switch
            {
                <= 30 => questTemplateEasy?.Id ?? string.Empty,
                <= 60 => questTemplateMedium?.Id ?? string.Empty,
                _ => questTemplateHard?.Id ?? string.Empty
            };

            dailyQuest.PlayerId = player.Id;
            dailyQuest.DateAssigned = DateTime.UtcNow;
            playerDailyQuests.Add(dailyQuest);
        }

        await _dailyQuestService.CreateAndAssignDailyQuestToUsers(playerDailyQuests);
    }

    private static QuestTemplateDto? GetQuestTemplateBasedOnDifficulty(List<QuestTemplateDto> questTemplates,
        QuestDifficultyEnum difficulty)
    {
        if (!Enum.IsDefined(difficulty))
            throw new InvalidEnumArgumentException(nameof(difficulty), (int)difficulty, typeof(QuestDifficultyEnum));

        return questTemplates.FirstOrDefault(qt => qt.Difficulty == difficulty);
    }
}