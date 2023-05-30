namespace LearningBuddy.Domain.Quizzes.Entities
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public short MaxPoints { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public User User { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Attempt> Attempts { get; set; }
    }
}
