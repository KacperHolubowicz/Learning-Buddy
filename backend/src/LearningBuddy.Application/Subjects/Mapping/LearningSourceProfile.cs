using AutoMapper;
using LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources;
using LearningBuddy.Application.Subjects.Queries.GetListOfPrivateLearningSources;
using LearningBuddy.Domain.Subjects.Entities;

namespace LearningBuddy.Application.Subjects.Mapping
{
    public class LearningSourceProfile : Profile
    {
        public LearningSourceProfile()
        {
            CreateMap<LearningSource, LearningSourceDTO>()
                .ForMember(dst => dst.Type,
                opt => opt.MapFrom(src => src.Type.ToString()));
            CreateMap<LearningSource, PrivateLearningSourceDTO>()
                .ForMember(dst => dst.Type,
                opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
