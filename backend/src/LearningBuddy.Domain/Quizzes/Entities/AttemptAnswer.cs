namespace LearningBuddy.Domain.Quizzes.Entities
{
    public class AttemptAnswer : BaseEntity
    {
        public Attempt Attempt { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
