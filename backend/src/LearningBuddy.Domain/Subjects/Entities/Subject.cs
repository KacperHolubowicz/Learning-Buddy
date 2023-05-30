namespace LearningBuddy.Domain.Subjects.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool Public { get; set; } = false;
        public bool Finished { get; set; } = false;
        public User Creator { get; set; }
        public byte[]? Thumbnail { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<SubjectTask> Tasks { get; set; }
        public ICollection<LearningSource> Sources { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
