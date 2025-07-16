using AutoMapper;
using ZeroToHeroAPI.Dtos;
using ZeroToHeroAPI.Models;

namespace ZeroToHeroAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Player, PlayerDto>();
        CreateMap<PlayerHistory, PlayerHistoryDto>();
        CreateMap<QuestTemplate, QuestTemplateDto>();
        CreateMap<QuestAction, QuestActionDto>();
        CreateMap<QuestReward, QuestRewardDto>();
        CreateMap<QuestPunishment, QuestPunishmentDto>();
        CreateMap<DailyQuest, DailyQuestDto>().ForMember(dest => dest.QuestActionProgress,
            opt => opt.MapFrom(src => src.ActionProgresses));
        CreateMap<QuestActionProgress, QuestActionProgressDto>();
    }
}