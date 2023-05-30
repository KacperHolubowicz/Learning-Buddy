namespace LearningBuddy.Domain.Quizzes.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; } = "";
        public byte Points { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Quiz Quiz { get; set; }
    }
}
