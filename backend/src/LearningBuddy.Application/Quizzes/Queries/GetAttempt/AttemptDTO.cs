using LearningBuddy.Domain.Quizzes.Entities;

namespace LearningBuddy.Application.Quizzes.Queries.GetAttempt
{
    public class AttemptDTO
    {
        public short Points { get; set; }
        public short MaxPoints { get; set; }
        public DateTimeOffset AttemptedAt { get; set; }
        public ICollection<AttemptAnswerDTO> Answers { get; set; }
    }
}