namespace LearningBuddy.Domain.Quizzes.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = "";
        public bool Correct { get; set; }
        public Question Question { get; set; }
    }
}