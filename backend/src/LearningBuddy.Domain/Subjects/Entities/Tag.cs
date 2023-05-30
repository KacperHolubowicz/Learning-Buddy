namespace LearningBuddy.Domain.Subjects.Entities
{
    public class Tag : BaseEntity
    {
        public string Value { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}