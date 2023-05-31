using LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes;
using LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjects;

namespace LearningBuddy.Application.Users.Queries.GetFavourite
{
    public class GetFavouriteResponse
    {
        public ICollection<SubjectItemDTO> Subjects{ get; set; }
        public ICollection<QuizItemDTO> Quizzes { get; set; }
        public ICollection<LearningSourceDTO> LearningSources { get; set; }
    }
}
