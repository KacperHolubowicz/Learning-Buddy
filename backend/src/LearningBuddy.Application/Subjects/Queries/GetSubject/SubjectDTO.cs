namespace LearningBuddy.Application.Subjects.Queries.GetSubject
{
    public class SubjectDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Public { get; set; }
        public bool Finished { get; set; }
        public string CreatorUsername { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
