namespace LearningBuddy.Application.Quizzes.Commands.AttemptCommands.CreateAttempt
{
    public class AttemptCreateResponseDTO
    {
        public long? ID { get; set; }
        public short Points { get; set; }
        public short MaxPoints { get; set; }
        public DateTimeOffset AttemptedAt { get; set; }
    }
}