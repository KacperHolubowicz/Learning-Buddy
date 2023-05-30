using AutoMapper;
using LearningBuddy.Application.Quizzes.Queries.GetAttempt;
using LearningBuddy.Application.Quizzes.Queries.GetListOfAttempts;
using LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions;
using LearningBuddy.Application.Quizzes.Queries.GetListOfQuestionsWithAnswers;
using LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes;
using LearningBuddy.Application.Quizzes.Queries.GetQuiz;
using LearningBuddy.Domain.Quizzes.Entities;

namespace LearningBuddy.Application.Quizzes.Mapping
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizItemDTO>()
                .ForMember(dst => dst.QuestionCount,
                opt => opt.MapFrom(src => src.Questions.Count));
            CreateMap<Quiz, QuizDTO>()
                .ForMember(dst => dst.QuestionsCount,
                opt => opt.MapFrom(src => src.Questions.Count))
                .ForMember(dst => dst.AttemptsCount,
                opt => opt.MapFrom(src => src.Attempts.Count));
            CreateMap<Answer, AnswerItemDTO>();
            CreateMap<Question, QuestionItemDTO>();
            CreateMap<Answer, AnswerWithCorrectnessItemDTO>();
            CreateMap<Question, QuestionWithAnswerItemDTO>();
            CreateMap<Attempt, AttemptItemDTO>();
            CreateMap<Attempt, AttemptDTO>();
            CreateMap<AttemptAnswer, AttemptAnswerDTO>();
        }
    }
}
