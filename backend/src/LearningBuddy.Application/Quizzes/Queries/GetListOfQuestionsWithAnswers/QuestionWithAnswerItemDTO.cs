using LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestionsWithAnswers
{
    public class QuestionWithAnswerItemDTO
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public byte Points { get; set; }
        public ICollection<AnswerWithCorrectnessItemDTO> Answers { get; set; }
    }
}