namespace LearningBuddy.Application.Quizzes.Queries.GetListOfAttempts
{
    public class AttemptItemDTO
    {
        public long ID { get; set; }
        public short Points { get; set; }
        public short MaxPoints { get; set; }
        public DateTimeOffset AttemptedAt { get; set; }
    }
}