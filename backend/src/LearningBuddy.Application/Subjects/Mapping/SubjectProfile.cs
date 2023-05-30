using AutoMapper;
using LearningBuddy.Application.Subjects.Queries.GetListOfPrivateSubjects;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjects;
using LearningBuddy.Application.Subjects.Queries.GetSubject;
using LearningBuddy.Domain.Subjects.Entities;

namespace LearningBuddy.Application.Subjects.Mapping
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectItemDTO>()
                .ForMember(dst => dst.Tags,
                opt => opt.MapFrom(src => Enumerable.AsEnumerable(src.Tags.Select(t => t.Value))));

            CreateMap<Subject, PrivateSubjectItemDTO>()
                .ForMember(dst => dst.Tags,
                opt => opt.MapFrom(src => Enumerable.AsEnumerable(src.Tags.Select(t => t.Value))));

            CreateMap<Subject, SubjectDTO>();

        }
    }
}
