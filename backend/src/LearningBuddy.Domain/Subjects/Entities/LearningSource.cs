namespace LearningBuddy.Domain.Subjects.Entities
{
    public class LearningSource : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool Public { get; set; }
        public SourceType Type { get; set; }
        public User User { get; set; }
        public Subject Subject { get; set; }
    }
}
