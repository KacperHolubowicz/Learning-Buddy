namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes
{
    public class QuizItemDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string UserUsername { get; set; }
        public string SubjectName { get; set; }
        public short QuestionCount { get; set; }
    }
}
