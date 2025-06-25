using AutoMapper;
using ZeroToHeroAPI.Models.Dtos;
using ZeroToHeroAPI.Models.Quest;

namespace ZeroToHeroAPI.MappingProfiles;

public class QuestProfile : Profile
{
    public QuestProfile()
    {
        CreateMap<QuestTemplate, QuestTemplateDto>();
    }
}