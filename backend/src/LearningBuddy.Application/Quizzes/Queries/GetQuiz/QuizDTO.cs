namespace LearningBuddy.Application.Quizzes.Queries.GetQuiz
{
    public class QuizDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public short MaxPoints { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string UserUsername { get; set; }
        public string SubjectName { get; set; }
        public short QuestionsCount { get; set; }
        public short AttemptsCount { get; set; }
        public bool IsOwner { get; set; }
    }
}
