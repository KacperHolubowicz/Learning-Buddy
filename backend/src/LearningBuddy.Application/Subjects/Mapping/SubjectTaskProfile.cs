using AutoMapper;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjectTasks;
using LearningBuddy.Domain.Subjects.Entities;

namespace LearningBuddy.Application.Subjects.Mapping
{
    public class SubjectTaskProfile : Profile
    {
        public SubjectTaskProfile()
        {
            CreateMap<SubjectTask, SubjectTaskItemDTO>();
        }
    }
}
