namespace LearningBuddy.Application.Subjects.Queries.GetListOfSubjectTasks
{
    public class SubjectTaskItemDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Finished { get; set; }
        public byte Priority { get; set; }
        public byte Difficulty { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}
