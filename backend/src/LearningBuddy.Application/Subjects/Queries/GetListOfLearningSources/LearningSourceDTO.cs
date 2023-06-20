namespace LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources
{
    public class LearningSourceDTO
    {
        public long ID { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public string Type { get; set; }
        public bool IsOwner { get; set; }
    }
}
