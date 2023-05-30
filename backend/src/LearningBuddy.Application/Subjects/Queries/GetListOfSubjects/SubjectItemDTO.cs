namespace LearningBuddy.Application.Subjects.Queries.GetListOfSubjects
{
    public class SubjectItemDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public byte[] Thumbnail { get; set; }
        public bool Finished { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
