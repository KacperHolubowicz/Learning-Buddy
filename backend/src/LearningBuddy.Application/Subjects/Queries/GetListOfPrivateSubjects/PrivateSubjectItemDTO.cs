namespace LearningBuddy.Application.Subjects.Queries.GetListOfPrivateSubjects
{
    public class PrivateSubjectItemDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public byte[] Thumbnail { get; set; }
        public bool Finished { get; set; }
        public bool Public { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
