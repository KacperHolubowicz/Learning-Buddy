namespace LearningBuddy.Application.Subjects.Queries.GetListOfPrivateLearningSources
{
    public class PrivateLearningSourceDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
    }
}
