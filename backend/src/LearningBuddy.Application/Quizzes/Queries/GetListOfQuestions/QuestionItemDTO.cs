namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions
{
    public class QuestionItemDTO
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public byte Points { get; set; }
        public ICollection<AnswerItemDTO> Answers { get; set; }
    }
}
