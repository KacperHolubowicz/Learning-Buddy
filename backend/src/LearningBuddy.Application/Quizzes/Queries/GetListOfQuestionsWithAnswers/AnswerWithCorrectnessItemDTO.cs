namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestionsWithAnswers
{
    public class AnswerWithCorrectnessItemDTO
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public bool Correct { get; set; }
    }
}
