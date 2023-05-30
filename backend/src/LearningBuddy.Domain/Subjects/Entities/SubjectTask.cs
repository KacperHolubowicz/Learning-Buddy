namespace LearningBuddy.Domain.Subjects.Entities
{
    public class SubjectTask : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool Finished { get; set; } = false;
        public int Priority { get; set; }
        public int Difficulty { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public User User { get; set; }
        public Subject Subject { get; set; }
    }
}