namespace LearningBuddy.Domain.Quizzes.Entities
{
    public class Attempt : BaseEntity
    {
        public short Points { get; set; }
        public short MaxPoints { get; set; }
        public DateTimeOffset AttemptedAt { get; set; }
        public User User { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<AttemptAnswer> Answers { get; set; }
    }
}